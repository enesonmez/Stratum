using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.OperationClaims;

public interface IOperationClaimReadRepository : IAsyncReadRepository<OperationClaim, int>, IReadRepository<OperationClaim, int>
{
    
}