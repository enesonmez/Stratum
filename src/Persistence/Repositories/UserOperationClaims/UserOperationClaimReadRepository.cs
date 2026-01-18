using System.Linq.Expressions;
using Core.Persistence.Abstractions.Paging;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.UserOperationClaims;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.UserOperationClaims;

public class UserOperationClaimReadRepository : EfReadRepositoryBase<UserOperationClaim, Guid, BaseDbContext>,
    IUserOperationClaimReadRepository
{
    public UserOperationClaimReadRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<IPaginate<UserOperationClaim>> GetListWithDetailsAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null, 
        int index = 0,
        int size = 10,
        bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<UserOperationClaim> queryable = Query().
            Include(uoc => uoc.User).
            Include(uoc => uoc.OperationClaim);
        
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            orderBy(queryable);
        
        return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
    }
}