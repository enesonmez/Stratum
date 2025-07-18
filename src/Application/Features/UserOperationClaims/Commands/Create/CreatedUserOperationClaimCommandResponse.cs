using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreatedUserOperationClaimCommandResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int OperationClaimId { get; set; }
}