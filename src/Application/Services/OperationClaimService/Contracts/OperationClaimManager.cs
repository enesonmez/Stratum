using System.Linq.Expressions;
using Application.Repositories.OperationClaims;
using Application.Services.OperationClaimService.Rules;
using Core.Persistence.Paging;
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

    public async Task<IPaginate<OperationClaim>> GetListAsync(Expression<Func<OperationClaim, bool>>? predicate = null,
        Func<IQueryable<OperationClaim>, IOrderedQueryable<OperationClaim>>? orderBy = null,
        Func<IQueryable<OperationClaim>, IIncludableQueryable<OperationClaim, object>>? include = null, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IPaginate<OperationClaim> operationClaimList = await _operationClaimReadRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return operationClaimList;
    }
}