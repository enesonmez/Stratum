namespace Domain.Entities;

public class User : Core.Security.Entities.User<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
}