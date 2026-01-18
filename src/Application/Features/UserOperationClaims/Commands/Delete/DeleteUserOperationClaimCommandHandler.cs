using Application.Features.UserOperationClaims.Mappers;
using Domain.Entities;
using Domain.Repositories.UserOperationClaims;
using Domain.Services.UserOperationClaims;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommandRequest,
    DeletedUserOperationClaimCommandResponse>
{
    private readonly UserOperationClaimDomainService _userOperationClaimDomainService;
    private readonly IUserOperationClaimWriteRepository _userOperationClaimWriteRepository;
    private readonly UserOperationClaimMapper _mapper;

    public DeleteUserOperationClaimCommandHandler(
        UserOperationClaimDomainService userOperationClaimDomainService,
        IUserOperationClaimWriteRepository userOperationClaimWriteRepository,
        UserOperationClaimMapper mapper)
    {
        _userOperationClaimDomainService = userOperationClaimDomainService;
        _userOperationClaimWriteRepository = userOperationClaimWriteRepository;
        _mapper = mapper;
    }

    public async Task<DeletedUserOperationClaimCommandResponse> Handle(DeleteUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim userOperationClaim = await _userOperationClaimDomainService.DeleteUserOperationClaimAsync(request.Id);

        await _userOperationClaimWriteRepository.DeleteAsync(userOperationClaim, cancellationToken: cancellationToken);

        DeletedUserOperationClaimCommandResponse response = _mapper.ToDeletedUserOperationClaimResponse(userOperationClaim);
        return response;
    }
}