using Application.Features.UserOperationClaims.Rules;
using Application.Services.UserOperationClaimService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommandRequest,
    CreatedUserOperationClaimCommandResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;
    private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

    public CreateUserOperationClaimCommandHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper,
        UserOperationClaimBusinessRules userOperationClaimBusinessRules)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
        _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
    }

    public async Task<CreatedUserOperationClaimCommandResponse> Handle(CreateUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
            request.UserId, request.OperationClaimId);

        UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
        UserOperationClaim createdUserOperationClaim =
            await _userOperationClaimService.AddAsync(mappedUserOperationClaim);

        CreatedUserOperationClaimCommandResponse response =
            _mapper.Map<CreatedUserOperationClaimCommandResponse>(createdUserOperationClaim);
        return response;
    }
}