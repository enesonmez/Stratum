using Core.CrossCuttingConcerns.Exception.Types;
using Domain.Entities;
using Core.Domain.Services;
using Domain.Repositories.OperationClaims;
using Domain.Services.OperationClaims.Constants;

namespace Domain.Services.OperationClaims;

public class OperationClaimDomainService : IDomainService
{
    private readonly IOperationClaimReadRepository _operationClaimRepository;

    public OperationClaimDomainService(IOperationClaimReadRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }
    
    public async Task<OperationClaim> CreateOperationClaimAsync(string name)
    {
        await ValidateOperationClaimNameIsUniqueAsync(name);

        OperationClaim operationClaim = OperationClaim.Create(name);

        return operationClaim;
    }
    
    public async Task<OperationClaim> DeleteOperationClaimAsync(int id)
    {
        OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
            predicate: oc => oc.Id == id);

        ValidateOperationClaimExists(operationClaim);

        operationClaim!.Delete();

        return operationClaim;
    }
    
    public async Task<OperationClaim> UpdateOperationClaimAsync(int id, string name)
    {
        OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
            predicate: oc => oc.Id == id);

        ValidateOperationClaimExists(operationClaim);

        if (operationClaim!.Name != name)
        {
            await ValidateOperationClaimNameIsUniqueAsync(name);
        }

        operationClaim.Update(name);

        return operationClaim;
    }
    
    public async Task<OperationClaim> GetOperationClaimByIdAsync(int id)
    {
        OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
            predicate: oc => oc.Id == id,
            enableTracking: false);
        
        ValidateOperationClaimExists(operationClaim);

        return operationClaim!;
    }
    
    // --- Private Helper Methods ---
    private async Task ValidateOperationClaimNameIsUniqueAsync(string name)
    {
        bool exists = await _operationClaimRepository.AnyAsync(b => b.Name == name);

        if (exists)
        {
            throw new BusinessException(
                OperationClaimsMessages.AlreadyExists, 
                OperationClaimsMessages.SectionName
            );
        }
    }
    
    private void ValidateOperationClaimExists(OperationClaim? operationClaim)
    {
        if (operationClaim == null)
        {
            throw new BusinessException(
                OperationClaimsMessages.NotFound, 
                OperationClaimsMessages.SectionName
            );
        }
    }
}