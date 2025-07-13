using Application.Services.UserOperationClaimService.Contracts;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQueryRequest,
    GetListResponse<GetListUserOperationClaimListItemDto>>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;

    public GetListUserOperationClaimQueryHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListUserOperationClaimListItemDto>> Handle(
        GetListUserOperationClaimQueryRequest request, CancellationToken cancellationToken)
    {
        IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimService.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false
        );

        GetListResponse<GetListUserOperationClaimListItemDto> response =
            _mapper.Map<GetListResponse<GetListUserOperationClaimListItemDto>>(userOperationClaims);
        return response;
    }
}