using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.OperationClaimService.Contracts;

public interface IOperationClaimService
{
    Task<OperationClaim?> GetAsync(
        Expression<Func<OperationClaim, bool>> predicate,
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
    
    Task<IPaginate<OperationClaim>> GetListAsync(
        Expression<Func<OperationClaim, bool>>? predicate = null,
        Func<IQueryable<OperationClaim>, IOrderedQueryable<OperationClaim>>? orderBy = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    
    Task<OperationClaim> AddAsync(OperationClaim operationClaim);
    Task<OperationClaim> UpdateAsync(OperationClaim operationClaim);
    Task<OperationClaim> DeleteAsync(OperationClaim operationClaim, bool permanent = false);
}