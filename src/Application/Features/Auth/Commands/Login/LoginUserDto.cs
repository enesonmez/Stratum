using Core.Application.Dtos;

namespace Application.Features.Auth.Commands.Login;

public class LoginUserDto : IDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? AuthenticatorCode { get; set; }
}