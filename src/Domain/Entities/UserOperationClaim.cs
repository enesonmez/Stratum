namespace Domain.Entities;

public class UserOperationClaim : Core.Security.Abstractions.Entities.UserOperationClaim<Guid, Guid, int>
{
    public virtual User User { get; set; } = null!;
    public virtual OperationClaim OperationClaim { get; set; } = null!;
    
    public UserOperationClaim()
    {
    }
    
    private UserOperationClaim(Guid userId, int operationClaimId)
    {
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
    
    public static UserOperationClaim Create(Guid userId, int operationClaimId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        
        if (operationClaimId <= 0)
            throw new ArgumentException("OperationClaimId must be valid.", nameof(operationClaimId));

        return new UserOperationClaim(userId, operationClaimId);
    }
    
    public void Delete()
    {}
}