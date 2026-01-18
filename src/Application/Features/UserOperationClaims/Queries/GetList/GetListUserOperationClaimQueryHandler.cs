using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;
using Domain.Repositories.UserOperationClaims;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQueryRequest,
    GetListResponse<GetListUserOperationClaimListItemDto>>
{
    private readonly IUserOperationClaimReadRepository _userOperationClaimReadRepository;
    private readonly IMapper _mapper;

    public GetListUserOperationClaimQueryHandler(IUserOperationClaimReadRepository userOperationClaimReadRepository, IMapper mapper)
    {
        _userOperationClaimReadRepository = userOperationClaimReadRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListUserOperationClaimListItemDto>> Handle(
        GetListUserOperationClaimQueryRequest request, CancellationToken cancellationToken)
    {
        IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimReadRepository.GetListWithDetailsAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        GetListResponse<GetListUserOperationClaimListItemDto> response =
            _mapper.Map<GetListResponse<GetListUserOperationClaimListItemDto>>(userOperationClaims);
        
        return response;
    }
}