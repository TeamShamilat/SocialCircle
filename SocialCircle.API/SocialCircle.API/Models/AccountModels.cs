namespace SocialCircle.API.Models;

public class RegisterModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
