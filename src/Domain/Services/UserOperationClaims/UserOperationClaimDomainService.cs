using Core.CrossCuttingConcerns.Exception.Types;
using Core.Domain.Services;
using Domain.Entities;
using Domain.Repositories.UserOperationClaims;
using Domain.Services.UserOperationClaims.Constants;

namespace Domain.Services.UserOperationClaims;

public class UserOperationClaimDomainService : IDomainService
{
    private readonly IUserOperationClaimReadRepository _userOperationClaimReadRepository;

    public UserOperationClaimDomainService(IUserOperationClaimReadRepository userOperationClaimReadRepository)
    {
        _userOperationClaimReadRepository = userOperationClaimReadRepository;
    }
    
    public async Task<UserOperationClaim> CreateUserOperationClaimAsync(Guid userId, int operationClaimId)
    {
        await ValidUserOperationClaimDoesNotExistsAsync(userId, operationClaimId);

        UserOperationClaim userOperationClaim = UserOperationClaim.Create(userId, operationClaimId);

        return userOperationClaim;
    }
    
    public async Task<UserOperationClaim> DeleteUserOperationClaimAsync(Guid id)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimReadRepository.GetAsync(
            predicate: uoc => uoc.Id == id);

        ValidateUserOperationClaimExists(userOperationClaim);

        userOperationClaim!.Delete();

        return userOperationClaim;
    }
    
    public async Task<UserOperationClaim> GetUserOperationClaimByIdAsync(Guid id)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimReadRepository.GetAsync(
            predicate: u => u.Id == id,
            enableTracking: false);

        ValidateUserOperationClaimExists(userOperationClaim);

        return userOperationClaim!;
    }
    
    // --- Private Helper Methods ---
    private async Task ValidUserOperationClaimDoesNotExistsAsync(Guid userId, int operationClaimId)
    {
        bool exists = await _userOperationClaimReadRepository.AnyAsync(
            uoc => uoc.UserId == userId && uoc.OperationClaimId == operationClaimId
        );

        if (exists)
        {
            throw new BusinessException(
                UserOperationClaimsMessages.UserOperationClaimAlreadyExists, 
                UserOperationClaimsMessages.SectionName
            );
        }
    }
    
    private void ValidateUserOperationClaimExists(UserOperationClaim? userOperationClaim)
    {
        if (userOperationClaim == null)
        {
            throw new BusinessException(
                UserOperationClaimsMessages.UserOperationClaimNotExists, 
                UserOperationClaimsMessages.SectionName
            );
        }
    }
}