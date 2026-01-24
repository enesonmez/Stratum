using Core.Application.Dtos;

namespace Application.Features.Auth.Commands.Register;

public class RegisterUserDto : IDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }  
    public required string LastName { get; set; }  
}