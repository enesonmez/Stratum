namespace Domain.Entities;

public class OperationClaim : Core.Security.Abstractions.Entities.OperationClaim<int>
{
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null!;
    
    public OperationClaim()
    {
    }
    public OperationClaim(int operationClaimId, string name): base(operationClaimId, name)
    {}
    
    private OperationClaim(string name) : base(name)
    {
    }
    
    public static OperationClaim Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Operation claim name cannot be empty.", nameof(name));

        return new OperationClaim(name);
    }

    public void Delete()
    {}
    
    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Operation claim name cannot be empty.", nameof(name));

        Name = name;
    }
}