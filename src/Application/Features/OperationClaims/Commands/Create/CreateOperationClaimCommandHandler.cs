using AutoMapper;
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
    private readonly IMapper _mapper;

    public CreateOperationClaimCommandHandler(
        OperationClaimDomainService operationClaimDomainService,
        IOperationClaimWriteRepository operationClaimWriteRepository,
        IMapper mapper)
    {
        _operationClaimDomainService = operationClaimDomainService;
        _operationClaimWriteRepository = operationClaimWriteRepository;
        _mapper = mapper;
    }

    public async Task<CreatedOperationClaimCommandResponse> Handle(CreateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim = await _operationClaimDomainService.CreateOperationClaimAsync(request.Name);

        await _operationClaimWriteRepository.AddAsync(operationClaim, cancellationToken);

        CreatedOperationClaimCommandResponse response = _mapper.Map<CreatedOperationClaimCommandResponse>(operationClaim);
        return response;
    }
}