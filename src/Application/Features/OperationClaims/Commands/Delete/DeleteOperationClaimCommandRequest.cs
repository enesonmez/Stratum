using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommandRequest : IRequest<DeletedOperationClaimCommandResponse>, ITransactionalRequest
{
    public int Id { get; set; }
}