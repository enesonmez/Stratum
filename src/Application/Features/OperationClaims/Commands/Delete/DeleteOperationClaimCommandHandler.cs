using Application.Features.OperationClaims.Rules;
using Application.Services.OperationClaimService;
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
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public DeleteOperationClaimCommandHandler(IOperationClaimService operationClaimService, IMapper mapper,
        OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _operationClaimService = operationClaimService;
        _mapper = mapper;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<DeletedOperationClaimCommandResponse> Handle(DeleteOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim? operationClaim = await _operationClaimService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

        OperationClaim deletedOperationClaim = await _operationClaimService.DeleteAsync(operationClaim!);

        DeletedOperationClaimCommandResponse response =
            _mapper.Map<DeletedOperationClaimCommandResponse>(deletedOperationClaim);
        return response;
    }
}