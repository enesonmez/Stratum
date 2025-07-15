using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeletedUserOperationClaimCommandResponse : IResponse
{
    public Guid Id { get; set; }
}