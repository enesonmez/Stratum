using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimQueryRequest : IRequest<GetByIdUserOperationClaimQueryResponse>
{
    public Guid Id { get; set; }
    
    public GetByIdUserOperationClaimQueryRequest()
    {
        Id = Guid.Empty;
    }
    
    public GetByIdUserOperationClaimQueryRequest(Guid id)
    {
        Id = id;
    }
}