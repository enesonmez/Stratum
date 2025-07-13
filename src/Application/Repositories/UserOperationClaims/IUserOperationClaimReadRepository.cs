using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.UserOperationClaims;

public interface IUserOperationClaimReadRepository : IAsyncReadRepository<UserOperationClaim, Guid>,
    IReadRepository<UserOperationClaim, Guid>
{
}