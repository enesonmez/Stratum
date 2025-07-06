using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.OperationClaimService.Contracts;

public interface IOperationClaimService
{
    Task<OperationClaim?> GetAsync(
        Expression<Func<OperationClaim, bool>> predicate,
        Func<IQueryable<OperationClaim>, IIncludableQueryable<OperationClaim, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    
    Task<OperationClaim> GetByIdAsync(
        int id,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    ); 
}