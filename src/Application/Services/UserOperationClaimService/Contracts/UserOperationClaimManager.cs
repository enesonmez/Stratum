using System.Linq.Expressions;
using Application.Repositories.UserOperationClaims;
using Application.Services.UserOperationClaimService.Rules;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserOperationClaimService.Contracts;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimReadRepository _userOperationClaimReadRepository;
    private readonly IUserOperationClaimWriteRepository _userOperationClaimWriteRepository;
    private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

    public UserOperationClaimManager(IUserOperationClaimReadRepository userOperationClaimReadRepository,
        IUserOperationClaimWriteRepository userOperationClaimWriteRepository,
        UserOperationClaimBusinessRules userOperationClaimBusinessRules)
    {
        _userOperationClaimReadRepository = userOperationClaimReadRepository;
        _userOperationClaimWriteRepository = userOperationClaimWriteRepository;
        _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
    }

    public async Task<UserOperationClaim?> GetAsync(Expression<Func<UserOperationClaim, bool>> predicate,
        Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
        bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        return await _userOperationClaimReadRepository.GetAsync(predicate, include, withDeleted, enableTracking,
            cancellationToken);
    }

    public async Task<UserOperationClaim> GetByIdAsync(Guid id, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        UserOperationClaim? userOperationClaim = await GetAsync(uoc => uoc.Id.Equals(id), withDeleted: withDeleted,
            enableTracking: enableTracking, cancellationToken: cancellationToken);
        
        await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);
        
        return userOperationClaim!;
    }
}