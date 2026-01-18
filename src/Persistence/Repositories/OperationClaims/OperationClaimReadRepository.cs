using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.OperationClaims;
using Persistence.Contexts;

namespace Persistence.Repositories.OperationClaims;

public class OperationClaimReadRepository : EfReadRepositoryBase<OperationClaim, int, BaseDbContext>,
    IOperationClaimReadRepository
{
    public OperationClaimReadRepository(BaseDbContext context) : base(context)
    {
    }
}