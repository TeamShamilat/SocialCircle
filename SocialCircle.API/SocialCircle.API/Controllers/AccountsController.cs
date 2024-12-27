using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialCircle.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SocialCircle.API.Controllers;

[ApiController]
[Route("api/Accounts")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AccountsController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email
        };

        //email
        //signInManager
        //usingn identity how to restrict 
        var result = await _userManager.CreateAsync(user, model.Password);


        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized("Invalid login credentials.");
        }

        var jwtToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        });
    }


    //[HttpPost("Register")]
    //public IActionResult Reggister([FromBody] RegisterModel model)
    //{
    //    // TODO: name, email, passowrd 
    //    // TODO: create new user
    //    /*
    //    1. email and passowrd required  check
    //    2. duplicate email check
    //    3. password -> hashPassword. 
    //    4. return new user
    //     */
    //    return Ok(
    //        new
    //        {
    //            Message = "User register succefully",
    //            model
    //        });
    //    // TODO: Register user
    //    return Accepted();
    //}

    //[HttpPost("Login")]
    //public IActionResult Login([FromBody] LoginModel model)
    //{
    //    // TODO: add functionality here (Zohaib, Huzaifa)
    //    /*
    //     * 1. check existing user matching user name (return 404 if not found)
    //     * 2. check password againt hashedpassword
    //     *  a. matched -> create token, along with user info
    //     *  b. not-matched -> return bad request (400) with (invalid username or password)
    //     */
    //    return Ok(
    //        new
    //        {
    //            AccessToken = "Acces token here",
    //            UserInfo = new User
    //            {
    //                //TODO: logged-in user profile here

    //            },
    //            model
    //        });

    //    // TODO: Login user
    //    return Accepted();
    //} 


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequest model)
    {
        var user = await _userManager.Users
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == model.RefreshToken));

        if (user == null)
        {
            return Unauthorized("Invalid refresh token.");
        }

        var refreshToken = user.RefreshTokens.SingleOrDefault(t =>
            t.Token == model.RefreshToken && t.ExpiryDate >= DateTime.UtcNow);

        if (refreshToken == null)
        {
            return Unauthorized("Refresh token not found or has expired.");
        }

        if (refreshToken.IsUsed || refreshToken.IsRevoked)
        {
            return Unauthorized("Refresh token has already been used or revoked.");
        }

        refreshToken.IsUsed = true;
        await _userManager.UpdateAsync(user);

        var newJwtToken = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshTokens.Add(newRefreshToken);
        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Token = newJwtToken,
            RefreshToken = newRefreshToken.Token
        });
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15), // Access Token expiry (e.g. 15 mins)
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiryDate = DateTime.UtcNow.AddDays(7) // Refresh Token expiry (e.g. 7 days)
        };
    }

}

// TODO: Mohsin replace anonymous login with actual (LoginResponseModel) model.
class LoginResponseModel
{
    // TODO: Add properties here
}