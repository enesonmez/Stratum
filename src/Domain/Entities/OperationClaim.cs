namespace Domain.Entities;

public class OperationClaim : Core.Security.Entities.OperationClaim<int>
{
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null!;
}