using Application.Features.OperationClaims.Constants;
using Core.Application.Rules;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Services.OperationClaimService.Rules;

public class OperationClaimBusinessRules : BaseBusinessRules
{
    public OperationClaimBusinessRules(ILocalizationService localizationService) :
        base(localizationService)
    {
    }
    
    public async Task OperationClaimShouldExistWhenSelected(OperationClaim? operationClaim)
    {
        if (operationClaim == null)
            await ThrowBusinessException(OperationClaimsMessages.NotExists, OperationClaimsMessages.SectionName);
    }
}