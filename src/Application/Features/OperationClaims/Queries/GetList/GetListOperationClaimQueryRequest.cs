using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQueryRequest : IRequest<GetListResponse<GetListOperationClaimListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    
    public GetListOperationClaimQueryRequest()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListOperationClaimQueryRequest(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }
}