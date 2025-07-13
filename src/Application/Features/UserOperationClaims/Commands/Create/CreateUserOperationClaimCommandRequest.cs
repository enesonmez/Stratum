using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandRequest : IRequest<CreatedUserOperationClaimCommandResponse>, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public int OperationClaimId { get; set; }
}