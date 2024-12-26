using Microsoft.AspNetCore.Mvc;
using SocialCircle.API.Models;

namespace SocialCircle.API.Controllers;

[ApiController]
[Route("api/Accounts")]
public class AccountsController : ControllerBase
{
    [HttpPost("Register")]
    public IActionResult Reggister([FromBody] RegisterModel model)
    {
        // TODO: name, email, passowrd 
        // TODO: create new user
        /*
        1. email and passowrd required  check
        2. duplicate email check
        3. password -> hashPassword. 
        4. return new user
         */
        return Ok(
            new
            {
                Message = "User register succefully",
                model
            });
        // TODO: Register user
        return Accepted();
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // TODO: add functionality here (Zohaib, Huzaifa)
        /*
         * 1. check existing user matching user name (return 404 if not found)
         * 2. check password againt hashedpassword
         *  a. matched -> create token, along with user info
         *  b. not-matched -> return bad request (400) with (invalid username or password)
         */
        return Ok(
            new
            {
                AccessToken = "Acces token here",
                UserInfo = new User
                {
                    //TODO: logged-in user profile here
                },
                model
            });

        // TODO: Login user
        return Accepted();
    } 
}

// TODO: Mohsin replace anonymous login with actual (LoginResponseModel) model.
class LoginResponseModel
{
    // TODO: Add properties here
}