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
        return Ok(
            new
            {
                Message = "User register succefully"
            });
        // TODO: Register user
        return Accepted();
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        return Ok(
    new
    {
        Message = "User logged-in succefully"
    });

        // TODO: Login user
        return Accepted();
    }
    
    
    
}


