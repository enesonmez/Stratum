using Application.Features.OperationClaims.Mappers;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;
using Domain.Repositories.OperationClaims;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQueryRequest,
    GetListResponse<GetListOperationClaimListItemDto>>
{
    private readonly IOperationClaimReadRepository _operationClaimReadRepository;
    private readonly OperationClaimMapper _operationClaimMapper;

    public GetListOperationClaimQueryHandler(
        IOperationClaimReadRepository operationClaimReadRepository, 
        OperationClaimMapper operationClaimMapper)
    {
        _operationClaimReadRepository = operationClaimReadRepository;
        _operationClaimMapper = operationClaimMapper;
    }

    public async Task<GetListResponse<GetListOperationClaimListItemDto>> Handle(GetListOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        IPaginate<OperationClaim> operationClaims = await _operationClaimReadRepository.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        var response = _operationClaimMapper.ToGetListOperationClaimResponse(operationClaims);
        return response;
    }
}