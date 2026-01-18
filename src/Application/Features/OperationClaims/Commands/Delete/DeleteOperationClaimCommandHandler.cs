using Application.Features.OperationClaims.Mappers;
using Domain.Entities;
using Domain.Repositories.OperationClaims;
using Domain.Services.OperationClaims;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Delete;

public class
    DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommandRequest,
    DeletedOperationClaimCommandResponse>
{
    private readonly OperationClaimDomainService _operationClaimDomainService;
    private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;
    private readonly OperationClaimMapper _operationClaimMapper;

    public DeleteOperationClaimCommandHandler(
        OperationClaimDomainService operationClaimDomainService,
        IOperationClaimWriteRepository operationClaimWriteRepository,
        OperationClaimMapper operationClaimMapper)
    {
        _operationClaimDomainService = operationClaimDomainService;
        _operationClaimWriteRepository = operationClaimWriteRepository;
        _operationClaimMapper = operationClaimMapper;
    }

    public async Task<DeletedOperationClaimCommandResponse> Handle(DeleteOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaimToDelete = await _operationClaimDomainService.DeleteOperationClaimAsync(request.Id);

        await _operationClaimWriteRepository.DeleteAsync(operationClaimToDelete, cancellationToken: cancellationToken);

        DeletedOperationClaimCommandResponse response = _operationClaimMapper.ToDeletedOperationClaimResponse(operationClaimToDelete);
        return response;
    }
}