using Application.Services.OperationClaimService;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQueryRequest,
    GetListResponse<GetListOperationClaimListItemDto>>
{
    private readonly IOperationClaimService _operationClaimService;
    private readonly IMapper _mapper;

    public GetListOperationClaimQueryHandler(IOperationClaimService operationClaimService, IMapper mapper)
    {
        _operationClaimService = operationClaimService;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListOperationClaimListItemDto>> Handle(GetListOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        IPaginate<OperationClaim> operationClaims = await _operationClaimService.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        
        return _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(operationClaims);
    }
}