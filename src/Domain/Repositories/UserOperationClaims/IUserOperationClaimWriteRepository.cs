using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.UserOperationClaims;

public interface IUserOperationClaimWriteRepository : IAsyncWriteRepository<UserOperationClaim, Guid>,
    IWriteRepository<UserOperationClaim, Guid>
{
}