using Core.Application.Responses;

namespace Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQueryResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetByIdOperationClaimQueryResponse()
    {
        Name = string.Empty;
    }

    public GetByIdOperationClaimQueryResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}