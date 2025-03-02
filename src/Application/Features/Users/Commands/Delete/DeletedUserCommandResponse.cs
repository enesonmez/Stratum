using Core.Application.Responses;

namespace Application.Features.Users.Commands.Delete;

public class DeletedUserCommandResponse : IResponse
{
    public Guid Id { get; set; }
    
    public DeletedUserCommandResponse()
    {
        Id = Guid.Empty;
    }

    public DeletedUserCommandResponse(Guid id)
    {
        Id = id;
    }
}