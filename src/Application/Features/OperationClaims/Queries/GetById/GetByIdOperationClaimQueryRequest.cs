using MediatR;

namespace Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQueryRequest : IRequest<GetByIdOperationClaimQueryResponse>
{
    public int Id { get; set; }
    
    public GetByIdOperationClaimQueryRequest()
    {
        Id = 0;
    }
    
    public GetByIdOperationClaimQueryRequest(int id)
    {
        Id = id;
    }
}