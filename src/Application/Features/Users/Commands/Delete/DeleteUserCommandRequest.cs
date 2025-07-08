using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandRequest : IRequest<DeletedUserCommandResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }

    public DeleteUserCommandRequest()
    {
        Id = Guid.Empty;
    }

    public DeleteUserCommandRequest(Guid id)
    {
        Id = id;
    }
}