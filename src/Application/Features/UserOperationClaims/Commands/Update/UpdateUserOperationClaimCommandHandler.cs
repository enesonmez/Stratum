using Application.Services.UserOperationClaimService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommandRequest,
    UpdatedUserOperationClaimCommandResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;

    public UpdateUserOperationClaimCommandHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
    }

    public async Task<UpdatedUserOperationClaimCommandResponse> Handle(UpdateUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim userOperationClaim =
            await _userOperationClaimService.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        UserOperationClaim mappedUserOperationClaim = _mapper.Map(request, userOperationClaim);

        UserOperationClaim updatedUserOperationClaim =
            await _userOperationClaimService.UpdateAsync(mappedUserOperationClaim);

        UpdatedUserOperationClaimCommandResponse response =
            _mapper.Map<UpdatedUserOperationClaimCommandResponse>(updatedUserOperationClaim);
        return response;
    }
}