using MediatR;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQueryRequest : IRequest<GetByIdUserQueryResponse>
{
    public Guid Id { get; set; }
    
    public GetByIdUserQueryRequest()
    {
        Id = Guid.Empty;
    }

    public GetByIdUserQueryRequest(Guid id)
    {
        Id = id;
    }
}