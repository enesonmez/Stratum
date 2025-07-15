using Application.Features.UserOperationClaims.Rules;
using Application.Services.UserOperationClaimService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommandRequest,
    DeletedUserOperationClaimCommandResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;
    private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

    public DeleteUserOperationClaimCommandHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper,
        UserOperationClaimBusinessRules userOperationClaimBusinessRules)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
        _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
    }

    public async Task<DeletedUserOperationClaimCommandResponse> Handle(DeleteUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);
        
        await _userOperationClaimService.DeleteAsync(userOperationClaim!);
        DeletedUserOperationClaimCommandResponse response =
            _mapper.Map<DeletedUserOperationClaimCommandResponse>(userOperationClaim);
        return response;
    }
}