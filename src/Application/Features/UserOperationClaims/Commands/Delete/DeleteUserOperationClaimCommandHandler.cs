using Application.Services.UserOperationClaimService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommandRequest,
    DeletedUserOperationClaimCommandResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;

    public DeleteUserOperationClaimCommandHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
    }

    public async Task<DeletedUserOperationClaimCommandResponse> Handle(DeleteUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim userOperationClaim =
            await _userOperationClaimService.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        
        await _userOperationClaimService.DeleteAsync(userOperationClaim);
        DeletedUserOperationClaimCommandResponse response =
            _mapper.Map<DeletedUserOperationClaimCommandResponse>(userOperationClaim);
        return response;
    }
}