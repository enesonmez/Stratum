using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.OperationClaims;

public interface IOperationClaimWriteRepository : IAsyncWriteRepository<OperationClaim, int>, 
    IWriteRepository<OperationClaim, int>
{
    
}