using Application.Features.OperationClaims.Rules;
using Application.Services.OperationClaimService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommandRequest,
    CreatedOperationClaimCommandResponse>
{
    private readonly IOperationClaimService _operationClaimService;
    private readonly IMapper _mapper;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public CreateOperationClaimCommandHandler(IOperationClaimService operationClaimService, IMapper mapper,
        OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _operationClaimService = operationClaimService;
        _mapper = mapper;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<CreatedOperationClaimCommandResponse> Handle(CreateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenCreating(request.Name);
        
        OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
        OperationClaim createdOperationClaim = await _operationClaimService.AddAsync(operationClaim);

        CreatedOperationClaimCommandResponse response =
            _mapper.Map<CreatedOperationClaimCommandResponse>(createdOperationClaim);
        return response;
    }
}