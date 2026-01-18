using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.OperationClaims;
using Persistence.Contexts;

namespace Persistence.Repositories.OperationClaims;

public class OperationClaimWriteRepository : EfWriteRepositoryBase<OperationClaim, int, BaseDbContext>,
    IOperationClaimWriteRepository
{
    public OperationClaimWriteRepository(BaseDbContext context) : base(context)
    {
    }
}