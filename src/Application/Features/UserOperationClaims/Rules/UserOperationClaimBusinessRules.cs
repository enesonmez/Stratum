using Application.Features.UserOperationClaims.Constants;
using Application.Repositories.UserOperationClaims;
using Core.Application.Rules;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IUserOperationClaimReadRepository _userOperationClaimReadRepository;

    public UserOperationClaimBusinessRules(ILocalizationService localizationService,
        IUserOperationClaimReadRepository userOperationClaimReadRepository) : base(localizationService)
    {
        _userOperationClaimReadRepository = userOperationClaimReadRepository;
    }

    public async Task UserOperationClaimShouldExistWhenSelected(UserOperationClaim? userOperationClaim)
    {
        if (userOperationClaim == null)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists,
                UserOperationClaimsMessages.SectionName);
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenInsert(Guid userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimReadRepository.AnyAsync(u =>
            u.UserId == userId && u.OperationClaimId == operationClaimId
        );
        if (doesExist)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists,
                UserOperationClaimsMessages.SectionName);
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenUpdated(Guid id, Guid userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimReadRepository.AnyAsync(predicate: uoc =>
            uoc.Id != id && uoc.UserId == userId && uoc.OperationClaimId == operationClaimId
        );
        if (doesExist)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists,
                UserOperationClaimsMessages.SectionName);
    }
}