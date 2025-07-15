using Application.Features.OperationClaims.Rules;
using Application.Services.OperationClaimService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Update;

public class
    UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommandRequest,
    UpdatedOperationClaimCommandResponse>
{
    private readonly IOperationClaimService _operationClaimService;
    private readonly IMapper _mapper;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public UpdateOperationClaimCommandHandler(IMapper mapper, IOperationClaimService operationClaimService,
        OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _mapper = mapper;
        _operationClaimService = operationClaimService;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<UpdatedOperationClaimCommandResponse> Handle(UpdateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim? operationClaim = await _operationClaimService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);
        await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenUpdating(operationClaim!.Id,
            request.Name);

        OperationClaim mappedOperationClaim = _mapper.Map(request, operationClaim);
        OperationClaim updatedOperationClaim = await _operationClaimService.UpdateAsync(mappedOperationClaim);

        UpdatedOperationClaimCommandResponse response =
            _mapper.Map<UpdatedOperationClaimCommandResponse>(updatedOperationClaim);
        return response;
    }
}