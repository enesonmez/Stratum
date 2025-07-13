using Application.Features.UserOperationClaims.Constants;
using Core.Application.Rules;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Services.UserOperationClaimService.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    public UserOperationClaimBusinessRules(ILocalizationService localizationService) : base(localizationService)
    {
    }

    public async Task UserOperationClaimShouldExistWhenSelected(UserOperationClaim? userOperationClaim)
    {
        if (userOperationClaim == null)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists,
                UserOperationClaimsMessages.SectionName);
    }
}