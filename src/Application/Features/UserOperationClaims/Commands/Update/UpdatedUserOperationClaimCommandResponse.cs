using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdatedUserOperationClaimCommandResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int OperationClaimId { get; set; }
}