using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreatedOperationClaimCommandResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CreatedOperationClaimCommandResponse()
    {
        Name = string.Empty;
    }

    public CreatedOperationClaimCommandResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}