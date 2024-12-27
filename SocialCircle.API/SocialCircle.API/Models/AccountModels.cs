using Microsoft.AspNetCore.Identity;

namespace SocialCircle.API.Models;

public class RegisterModel
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class TokenRequest
{
    public string RefreshToken { get; set; }
}

public class ApplicationUser : IdentityUser
{
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Revoked { get; set; }
}
