using Application.Features.OperationClaims.Mappers;
using Domain.Entities;
using Domain.Repositories.OperationClaims;
using Domain.Services.OperationClaims;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Update;

public class
    UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommandRequest,
    UpdatedOperationClaimCommandResponse>
{
    private readonly OperationClaimDomainService _operationClaimDomainService;
    private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;
    private readonly OperationClaimMapper _operationClaimMapper;

    public UpdateOperationClaimCommandHandler(
        OperationClaimDomainService operationClaimDomainService,
        IOperationClaimWriteRepository operationClaimWriteRepository,
        OperationClaimMapper operationClaimMapper)
    {
        _operationClaimDomainService = operationClaimDomainService;
        _operationClaimWriteRepository = operationClaimWriteRepository;
        _operationClaimMapper = operationClaimMapper;
    }

    public async Task<UpdatedOperationClaimCommandResponse> Handle(UpdateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim updatedOperationClaim = await _operationClaimDomainService.UpdateOperationClaimAsync(
            request.Id, 
            request.Name
        );

        await _operationClaimWriteRepository.UpdateAsync(updatedOperationClaim, cancellationToken);

        UpdatedOperationClaimCommandResponse response = _operationClaimMapper.ToUpdatedOperationClaimResponse(updatedOperationClaim);
        return response;
    }
}