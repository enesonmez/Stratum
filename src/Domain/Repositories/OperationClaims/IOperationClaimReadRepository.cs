using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.OperationClaims;

public interface IOperationClaimReadRepository : IAsyncReadRepository<OperationClaim, int>, 
    IReadRepository<OperationClaim, int>
{
    
}