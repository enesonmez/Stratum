using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommandRequest : IRequest<UpdatedUserOperationClaimCommandResponse>,
    ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int OperationClaimId { get; set; }
}