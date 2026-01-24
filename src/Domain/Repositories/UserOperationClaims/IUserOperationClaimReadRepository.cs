using System.Linq.Expressions;
using Core.Persistence.Abstractions.Paging;
using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.UserOperationClaims;

public interface IUserOperationClaimReadRepository : IAsyncReadRepository<UserOperationClaim, Guid>,
    IReadRepository<UserOperationClaim, Guid>
{
    Task<IPaginate<UserOperationClaim>> GetListWithDetailsAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    public Task<IList<OperationClaim>> GetOperationClaimsByUserIdAsync(Guid userId);
}