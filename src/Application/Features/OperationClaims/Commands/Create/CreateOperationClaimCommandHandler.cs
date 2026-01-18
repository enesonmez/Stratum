using Application.Features.OperationClaims.Mappers;
using Domain.Entities;
using Domain.Repositories.OperationClaims;
using Domain.Services.OperationClaims;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommandRequest,
    CreatedOperationClaimCommandResponse>
{
    private readonly OperationClaimDomainService _operationClaimDomainService;
    private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;
    private readonly OperationClaimMapper _operationClaimMapper;

    public CreateOperationClaimCommandHandler(
        OperationClaimDomainService operationClaimDomainService,
        IOperationClaimWriteRepository operationClaimWriteRepository,
        OperationClaimMapper operationClaimMapper)
    {
        _operationClaimDomainService = operationClaimDomainService;
        _operationClaimWriteRepository = operationClaimWriteRepository;
        _operationClaimMapper = operationClaimMapper;
    }

    public async Task<CreatedOperationClaimCommandResponse> Handle(CreateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim = await _operationClaimDomainService.CreateOperationClaimAsync(request.Name);

        await _operationClaimWriteRepository.AddAsync(operationClaim, cancellationToken);

        CreatedOperationClaimCommandResponse response = _operationClaimMapper.ToCreatedOperationClaimResponse(operationClaim);
        return response;
    }
}