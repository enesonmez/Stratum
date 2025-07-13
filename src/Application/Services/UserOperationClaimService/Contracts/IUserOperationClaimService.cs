using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserOperationClaimService.Contracts;

public interface IUserOperationClaimService
{
    Task<UserOperationClaim?> GetAsync(
        Expression<Func<UserOperationClaim, bool>> predicate,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    
    Task<UserOperationClaim> GetByIdAsync(
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
}