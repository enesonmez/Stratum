using System.Linq.Expressions;
using Application.Repositories.UserOperationClaims;
using Application.Services.UserOperationClaimService.Rules;
using Core.Persistence.Paging;
using Domain.Dtos;
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
        bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        return await _userOperationClaimReadRepository.GetAsync(
            predicate: predicate,
            withDeleted: withDeleted,
            enableTracking: enableTracking,
            cancellationToken: cancellationToken
        );
    }

    public async Task<UserOperationClaim> GetByIdAsync(Guid id, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        UserOperationClaim? userOperationClaim = await GetAsync(uoc => uoc.Id.Equals(id), withDeleted: withDeleted,
            enableTracking: enableTracking, cancellationToken: cancellationToken);

        await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);

        return userOperationClaim!;
    }

    public async Task<IPaginate<UserOperationClaim>> GetListAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null,
        int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        return await _userOperationClaimReadRepository.GetListAsync(
            predicate: predicate,
            orderBy: orderBy,
            index: index,
            size: size,
            withDeleted: withDeleted,
            enableTracking: enableTracking,
            cancellationToken: cancellationToken
        );
    }

    public async Task<IPaginate<UserOperationClaimListItemDto>> GetListUserOperationClaimDtoAsync(
        Expression<Func<UserOperationClaim, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy = null, int index = 0,
        int size = 10,
        bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        return await _userOperationClaimReadRepository.GetListUserOperationClaimDtoAsync(
            predicate: predicate,
            orderBy: orderBy,
            index: index,
            size: size,
            withDeleted: withDeleted,
            enableTracking: enableTracking,
            cancellationToken: cancellationToken
        );
    }

    public async Task<UserOperationClaim> AddAsync(UserOperationClaim userOperationClaim)
    {
        await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
            userOperationClaim.UserId, userOperationClaim.OperationClaimId);
        
        return await _userOperationClaimWriteRepository.AddAsync(userOperationClaim);
    }

    public async Task<UserOperationClaim> UpdateAsync(UserOperationClaim userOperationClaim)
    {
        await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenUpdated(
            userOperationClaim.Id,
            userOperationClaim.UserId, 
            userOperationClaim.OperationClaimId
        );
        
        return await _userOperationClaimWriteRepository.UpdateAsync(userOperationClaim);
    }
}