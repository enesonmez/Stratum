using AutoMapper;
using Domain.Entities;
using Domain.Services.OperationClaims;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetById;

public class
    GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQueryRequest,
    GetByIdOperationClaimQueryResponse>
{
    private readonly OperationClaimDomainService _operationClaimDomainService;
    private readonly IMapper _mapper;

    public GetByIdOperationClaimQueryHandler(
        OperationClaimDomainService operationClaimDomainService, 
        IMapper mapper)
    {
        _operationClaimDomainService = operationClaimDomainService;
        _mapper = mapper;
    }

    public async Task<GetByIdOperationClaimQueryResponse> Handle(GetByIdOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim = await _operationClaimDomainService.GetOperationClaimByIdAsync(request.Id);

        GetByIdOperationClaimQueryResponse response = _mapper.Map<GetByIdOperationClaimQueryResponse>(operationClaim);

        return response;
    }
}