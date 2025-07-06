using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.OperationClaims;

public interface IOperationClaimWriteRepository : IAsyncWriteRepository<OperationClaim, int>, IWriteRepository<OperationClaim, int>
{
    
}