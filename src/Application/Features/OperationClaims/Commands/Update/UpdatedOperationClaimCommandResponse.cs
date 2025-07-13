using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdatedOperationClaimCommandResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdatedOperationClaimCommandResponse()
    {
        Name = string.Empty;
    }

    public UpdatedOperationClaimCommandResponse(string name, int id)
    {
        Name = name;
        Id = id;
    }
}