using Application.Services.OperationClaimService.Contracts;
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

    public GetByIdOperationClaimQueryHandler(IOperationClaimService operationClaimService, IMapper mapper)
    {
        _operationClaimService = operationClaimService;
        _mapper = mapper;
    }

    public async Task<GetByIdOperationClaimQueryResponse> Handle(GetByIdOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim =
            await _operationClaimService.GetByIdAsync(
                request.Id,
                enableTracking: false,
                cancellationToken: cancellationToken
            );
        
        return _mapper.Map<GetByIdOperationClaimQueryResponse>(operationClaim);
    }
}