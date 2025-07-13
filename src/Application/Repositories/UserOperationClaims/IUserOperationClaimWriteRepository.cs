using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.UserOperationClaims;

public interface IUserOperationClaimWriteRepository : IAsyncWriteRepository<UserOperationClaim, Guid>,
    IWriteRepository<UserOperationClaim, Guid>
{
}