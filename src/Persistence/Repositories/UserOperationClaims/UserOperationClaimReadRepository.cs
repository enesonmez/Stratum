using System.Linq.Expressions;
using Application.Dtos;
using Application.Repositories.UserOperationClaims;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.UserOperationClaims;

public class UserOperationClaimReadRepository : EfReadRepositoryBase<UserOperationClaim, Guid, BaseDbContext>,
    IUserOperationClaimReadRepository
{
    public UserOperationClaimReadRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<IPaginate<UserOperationClaimListItemDto>> GetListUserOperationClaimDtoAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null, int index = 0,
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
        
        var dtoList = queryable.Select(uoc => new UserOperationClaimListItemDto
        {
            Id = uoc.Id,
            UserId = uoc.UserId,
            Email = uoc.User.Email,
            OperationClaimId = uoc.OperationClaimId,
            OperationClaimName = uoc.OperationClaim.Name
        });
        return await dtoList.ToPaginateAsync(index, size, from: 0, cancellationToken);
    }
}