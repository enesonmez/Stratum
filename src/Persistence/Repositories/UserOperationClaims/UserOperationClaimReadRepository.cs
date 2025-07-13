using Application.Repositories.UserOperationClaims;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.UserOperationClaims;

public class UserOperationClaimReadRepository : EfReadRepositoryBase<UserOperationClaim, Guid, BaseDbContext>,
    IUserOperationClaimReadRepository
{
    public UserOperationClaimReadRepository(BaseDbContext context) : base(context)
    {
    }
}