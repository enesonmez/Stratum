using Application.Features.UserOperationClaims.Rules;
using Application.Services.UserOperationClaimService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommandRequest,
    UpdatedUserOperationClaimCommandResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;
    private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

    public UpdateUserOperationClaimCommandHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper,
        UserOperationClaimBusinessRules userOperationClaimBusinessRules)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
        _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
    }

    public async Task<UpdatedUserOperationClaimCommandResponse> Handle(UpdateUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);
        await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenUpdated(userOperationClaim!.Id,
            request.UserId, request.OperationClaimId);

        UserOperationClaim mappedUserOperationClaim = _mapper.Map(request, userOperationClaim);

        UserOperationClaim updatedUserOperationClaim =
            await _userOperationClaimService.UpdateAsync(mappedUserOperationClaim);

        UpdatedUserOperationClaimCommandResponse response =
            _mapper.Map<UpdatedUserOperationClaimCommandResponse>(updatedUserOperationClaim);
        return response;
    }
}