using Application.Services.OperationClaimService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommandRequest,
    CreatedOperationClaimCommandResponse>
{
    private readonly IOperationClaimService _operationClaimService;
    private readonly IMapper _mapper;

    public CreateOperationClaimCommandHandler(IOperationClaimService operationClaimService, IMapper mapper)
    {
        _operationClaimService = operationClaimService;
        _mapper = mapper;
    }

    public async Task<CreatedOperationClaimCommandResponse> Handle(CreateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
        OperationClaim createdOperationClaim = await _operationClaimService.AddAsync(operationClaim);

        CreatedOperationClaimCommandResponse response =
            _mapper.Map<CreatedOperationClaimCommandResponse>(createdOperationClaim);
        return response;
    }
}