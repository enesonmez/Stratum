using Core.Application.Responses;
using Core.Security.Abstractions.Jwt;

namespace Application.Features.Auth.Commands.Register;

public class RegisteredCommandResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public Domain.Entities.RefreshToken RefreshToken { get; set; }

    public RegisteredCommandResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public RegisteredCommandResponse(AccessToken accessToken, Domain.Entities.RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

}