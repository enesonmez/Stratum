namespace Domain.Entities;

public class RefreshToken : Core.Security.Abstractions.Entities.RefreshToken<Guid, Guid>
{
    public virtual User User { get; set; } = null!;
    
    public RefreshToken()
    {
    }

    public RefreshToken(Guid id, string token, DateTime expiration)
    {
        Id = id;
        Token = token;
        ExpirationDate = expiration;
    }

    public void Revoke(string ipAddress, string reason, string? replacedByToken = null)
    {
        RevokedDate = DateTime.UtcNow;
        RevokedByIp = ipAddress;
        ReasonRevoked = reason;
        ReplacedByToken = replacedByToken;
    }
}