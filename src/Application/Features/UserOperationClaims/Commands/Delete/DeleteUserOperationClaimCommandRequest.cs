using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommandRequest : IRequest<DeletedUserOperationClaimCommandResponse>,
    ITransactionalRequest
{
    public Guid Id { get; set; }
}