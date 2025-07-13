using Application.Features.OperationClaims.Constants;
using Application.Repositories.OperationClaims;
using Core.Application.Rules;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Services.OperationClaimService.Rules;

public class OperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IOperationClaimReadRepository _operationClaimReadRepository;

    public OperationClaimBusinessRules(ILocalizationService localizationService,
        IOperationClaimReadRepository operationClaimReadRepository) :
        base(localizationService)
    {
        _operationClaimReadRepository = operationClaimReadRepository;
    }

    public async Task OperationClaimShouldExistWhenSelected(OperationClaim? operationClaim)
    {
        if (operationClaim == null)
            await ThrowBusinessException(OperationClaimsMessages.NotExists, OperationClaimsMessages.SectionName);
    }

    public async Task OperationClaimNameShouldNotExistWhenCreating(string name)
    {
        bool doesExist = await _operationClaimReadRepository.AnyAsync(predicate: b => b.Name == name);
        if (doesExist)
            await ThrowBusinessException(OperationClaimsMessages.AlreadyExists, OperationClaimsMessages.SectionName);
    }
}