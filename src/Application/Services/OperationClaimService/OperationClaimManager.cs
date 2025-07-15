using System.Linq.Expressions;
using Application.Repositories.OperationClaims;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Services.OperationClaimService;

public class OperationClaimManager : IOperationClaimService
{
    private readonly IOperationClaimReadRepository _operationClaimReadRepository;
    private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;

    public OperationClaimManager(IOperationClaimReadRepository operationClaimReadRepository,
        IOperationClaimWriteRepository operationClaimWriteRepository)
    {
        _operationClaimReadRepository = operationClaimReadRepository;
        _operationClaimWriteRepository = operationClaimWriteRepository;
    }

    public async Task<OperationClaim?> GetAsync(Expression<Func<OperationClaim, bool>> predicate,
        bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        OperationClaim? operationClaim = await _operationClaimReadRepository.GetAsync(
            predicate:predicate,
            withDeleted:withDeleted,
            enableTracking:enableTracking,
            cancellationToken:cancellationToken
        );
        return operationClaim;
    }

    public async Task<OperationClaim?> GetByIdAsync(int id, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        OperationClaim? operationClaim = await GetAsync(oc => oc.Id.Equals(id), withDeleted: withDeleted,
            enableTracking: enableTracking, cancellationToken: cancellationToken);

        return operationClaim;
    }

    public async Task<IPaginate<OperationClaim>> GetListAsync(Expression<Func<OperationClaim, bool>>? predicate = null,
        Func<IQueryable<OperationClaim>, IOrderedQueryable<OperationClaim>>? orderBy = null, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IPaginate<OperationClaim> operationClaimList = await _operationClaimReadRepository.GetListAsync(
            predicate:predicate,
            orderBy:orderBy,
            index:index,
            size:size,
            withDeleted:withDeleted,
            enableTracking:enableTracking,
            cancellationToken:cancellationToken
        );
        return operationClaimList;
    }

    public async Task<OperationClaim> AddAsync(OperationClaim operationClaim)
    {
        OperationClaim addedOperationClaim = await _operationClaimWriteRepository.AddAsync(operationClaim);
        return addedOperationClaim;
    }

    public async Task<OperationClaim> UpdateAsync(OperationClaim operationClaim)
    {
        OperationClaim updatedOperationClaim = await _operationClaimWriteRepository.UpdateAsync(operationClaim);
        return updatedOperationClaim;
    }

    public async Task<OperationClaim> DeleteAsync(OperationClaim operationClaim, bool permanent = false)
    {
        OperationClaim deletedOperationClaim =
            await _operationClaimWriteRepository.DeleteAsync(operationClaim, permanent);

        return deletedOperationClaim;
    }
}