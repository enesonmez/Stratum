using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.Delete;

public class DeletedOperationClaimCommandResponse : IResponse
{
    public int Id { get; set; }
}