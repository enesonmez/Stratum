using Core.Application.Responses;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserCommandResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public CreatedUserCommandResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public CreatedUserCommandResponse(Guid id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}