using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommandRequest : IRequest<UpdatedUserCommandResponse>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public UpdateUserCommandRequest()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public UpdateUserCommandRequest(Guid id, string firstName, string lastName, string email, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}