using System.Linq.Expressions;
using Application.Repositories.OperationClaims;
using Application.Services.OperationClaimService.Rules;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.OperationClaimService.Contracts;

public class OperationClaimManager : IOperationClaimService
{
    private readonly IOperationClaimReadRepository _operationClaimReadRepository;
    private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public OperationClaimManager(IOperationClaimReadRepository operationClaimReadRepository,
        IOperationClaimWriteRepository operationClaimWriteRepository,
        OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _operationClaimReadRepository = operationClaimReadRepository;
        _operationClaimWriteRepository = operationClaimWriteRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<OperationClaim?> GetAsync(Expression<Func<OperationClaim, bool>> predicate,
        Func<IQueryable<OperationClaim>, IIncludableQueryable<OperationClaim, object>>? include = null,
        bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        OperationClaim? operationClaim = await _operationClaimReadRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return operationClaim;
    }

    public async Task<OperationClaim> GetByIdAsync(int id, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        OperationClaim? operationClaim = await GetAsync(oc => oc.Id.Equals(id), withDeleted: withDeleted,
            enableTracking: enableTracking, cancellationToken: cancellationToken);

        await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);
        
        return operationClaim!;
    }
}