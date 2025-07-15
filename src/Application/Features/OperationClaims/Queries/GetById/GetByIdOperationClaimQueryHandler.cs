using Application.Features.OperationClaims.Rules;
using Application.Services.OperationClaimService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetById;

public class
    GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQueryRequest,
    GetByIdOperationClaimQueryResponse>
{
    private readonly IOperationClaimService _operationClaimService;
    private readonly IMapper _mapper;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public GetByIdOperationClaimQueryHandler(IOperationClaimService operationClaimService, IMapper mapper,
        OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _operationClaimService = operationClaimService;
        _mapper = mapper;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<GetByIdOperationClaimQueryResponse> Handle(GetByIdOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim? operationClaim = await _operationClaimService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

        return _mapper.Map<GetByIdOperationClaimQueryResponse>(operationClaim);
    }
}