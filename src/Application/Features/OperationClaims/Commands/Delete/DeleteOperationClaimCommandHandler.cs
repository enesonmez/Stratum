using Application.Services.OperationClaimService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Delete;

public class
    DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommandRequest,
    DeletedOperationClaimCommandResponse>
{
    private readonly IOperationClaimService _operationClaimService;
    private readonly IMapper _mapper;

    public DeleteOperationClaimCommandHandler(IOperationClaimService operationClaimService, IMapper mapper)
    {
        _operationClaimService = operationClaimService;
        _mapper = mapper;
    }

    public async Task<DeletedOperationClaimCommandResponse> Handle(DeleteOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim =
            await _operationClaimService.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        OperationClaim deletedOperationClaim = await _operationClaimService.DeleteAsync(operationClaim);

        DeletedOperationClaimCommandResponse response =
            _mapper.Map<DeletedOperationClaimCommandResponse>(deletedOperationClaim);
        return response;
    }
}