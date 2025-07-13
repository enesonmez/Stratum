using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserOperationClaimService.Contracts;

public interface IUserOperationClaimService
{
    Task<UserOperationClaim?> GetAsync(
        Expression<Func<UserOperationClaim, bool>> predicate,
        Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
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
}