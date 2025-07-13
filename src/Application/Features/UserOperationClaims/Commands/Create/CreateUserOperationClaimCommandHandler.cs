using Application.Services.OperationClaimService.Contracts;
using Application.Services.UserOperationClaimService.Contracts;
using Application.Services.UserService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommandRequest,
    CreatedUserOperationClaimCommandResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;

    public CreateUserOperationClaimCommandHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper,
        IUserService userService, IOperationClaimService operationClaimService)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
    }

    public async Task<CreatedUserOperationClaimCommandResponse> Handle(CreateUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
        UserOperationClaim createdUserOperationClaim =
            await _userOperationClaimService.AddAsync(mappedUserOperationClaim);

        CreatedUserOperationClaimCommandResponse response =
            _mapper.Map<CreatedUserOperationClaimCommandResponse>(createdUserOperationClaim);
        return response;
    }
}