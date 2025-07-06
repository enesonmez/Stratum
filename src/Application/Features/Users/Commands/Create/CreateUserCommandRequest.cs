using Core.Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandRequest : IRequest<CreatedUserCommandResponse>, ILoggableRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public CreateUserCommandRequest()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }

    public CreateUserCommandRequest(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
}