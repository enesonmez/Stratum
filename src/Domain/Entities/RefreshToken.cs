namespace Domain.Entities;

public class RefreshToken : Core.Security.Abstractions.Entities.RefreshToken<Guid, Guid>
{
    public virtual User User { get; set; } = null!;
}