using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQueryRequest : IRequest<GetListResponse<GetListUserOperationClaimListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    
    public GetListUserOperationClaimQueryRequest()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListUserOperationClaimQueryRequest(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }
}