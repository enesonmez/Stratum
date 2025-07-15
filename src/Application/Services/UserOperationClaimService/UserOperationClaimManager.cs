using System.Linq.Expressions;
using Application.Repositories.UserOperationClaims;
using Core.Persistence.Paging;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Services.UserOperationClaimService;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimReadRepository _userOperationClaimReadRepository;
    private readonly IUserOperationClaimWriteRepository _userOperationClaimWriteRepository;

    public UserOperationClaimManager(IUserOperationClaimReadRepository userOperationClaimReadRepository,
        IUserOperationClaimWriteRepository userOperationClaimWriteRepository)
    {
        _userOperationClaimReadRepository = userOperationClaimReadRepository;
        _userOperationClaimWriteRepository = userOperationClaimWriteRepository;
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

    public async Task<UserOperationClaim?> GetByIdAsync(Guid id, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        UserOperationClaim? userOperationClaim = await GetAsync(uoc => uoc.Id.Equals(id), withDeleted: withDeleted,
            enableTracking: enableTracking, cancellationToken: cancellationToken);

        return userOperationClaim;
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
        return await _userOperationClaimWriteRepository.AddAsync(userOperationClaim);
    }

    public async Task<UserOperationClaim> UpdateAsync(UserOperationClaim userOperationClaim)
    {
        return await _userOperationClaimWriteRepository.UpdateAsync(userOperationClaim);
    }

    public async Task<UserOperationClaim> DeleteAsync(UserOperationClaim userOperationClaim, bool permanent = false)
    {
        return await _userOperationClaimWriteRepository.DeleteAsync(userOperationClaim, permanent);
    }
}