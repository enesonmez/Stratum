using Application.Repositories.UserOperationClaims;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.UserOperationClaims;

public class UserOperationClaimWriteRepository : EfWriteRepositoryBase<UserOperationClaim, Guid, BaseDbContext>,
    IUserOperationClaimWriteRepository
{
    public UserOperationClaimWriteRepository(BaseDbContext context) : base(context)
    {
    }
}