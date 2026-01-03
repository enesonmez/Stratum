using System.Linq.Expressions;
using Application.Dtos;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Services.UserOperationClaimService;

public interface IUserOperationClaimService
{
    Task<UserOperationClaim?> GetAsync(
        Expression<Func<UserOperationClaim, bool>> predicate,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    
    Task<UserOperationClaim?> GetByIdAsync(
        Guid id,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    ); 
    
    Task<IPaginate<UserOperationClaim>> GetListAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    
    Task<IPaginate<UserOperationClaimListItemDto>> GetListUserOperationClaimDtoAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<UserOperationClaim> AddAsync(UserOperationClaim userOperationClaim);
    Task<UserOperationClaim> UpdateAsync(UserOperationClaim userOperationClaim);
    Task<UserOperationClaim> DeleteAsync(UserOperationClaim userOperationClaim, bool permanent = false);
}