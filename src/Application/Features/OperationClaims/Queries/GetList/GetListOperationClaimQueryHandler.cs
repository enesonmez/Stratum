using AutoMapper;
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
    private readonly IMapper _mapper;

    public GetListOperationClaimQueryHandler(
        IOperationClaimReadRepository operationClaimReadRepository, 
        IMapper mapper)
    {
        _operationClaimReadRepository = operationClaimReadRepository;
        _mapper = mapper;
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

        var response = _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(operationClaims);
        return response;
    }
}