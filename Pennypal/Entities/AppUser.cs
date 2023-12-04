namespace Pennypal.Entities;

public class AppUser: IdentityUser
{
    public string DisplayName { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    
}