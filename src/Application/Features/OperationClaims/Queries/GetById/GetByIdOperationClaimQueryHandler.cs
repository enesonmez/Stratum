using Application.Features.OperationClaims.Mappers;
using Domain.Entities;
using Domain.Services.OperationClaims;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetById;

public class
    GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQueryRequest,
    GetByIdOperationClaimQueryResponse>
{
    private readonly OperationClaimDomainService _operationClaimDomainService;
    private readonly OperationClaimMapper _operationClaimMapper;

    public GetByIdOperationClaimQueryHandler(
        OperationClaimDomainService operationClaimDomainService, 
        OperationClaimMapper operationClaimMapper)
    {
        _operationClaimDomainService = operationClaimDomainService;
        _operationClaimMapper = operationClaimMapper;
    }

    public async Task<GetByIdOperationClaimQueryResponse> Handle(GetByIdOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim = await _operationClaimDomainService.GetOperationClaimByIdAsync(request.Id);

        GetByIdOperationClaimQueryResponse response = _operationClaimMapper.ToGetByIdOperationClaimResponse(operationClaim);

        return response;
    }
}