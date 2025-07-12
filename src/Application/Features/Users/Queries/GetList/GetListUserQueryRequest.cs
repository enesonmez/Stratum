using Core.Application.Pipelines.Performance;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQueryRequest : IRequest<GetListResponse<GetListUserListItemDto>>, IPerformanceRequest
{
    public PageRequest PageRequest { get; set; }
    
    public GetListUserQueryRequest()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListUserQueryRequest(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    int IPerformanceRequest.Interval => 0;
}