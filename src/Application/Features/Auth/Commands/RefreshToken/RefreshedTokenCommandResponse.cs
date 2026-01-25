using Core.Security.Abstractions.Jwt;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshedTokenCommandResponse
{
    public AccessToken AccessToken { get; set; }
    public Domain.Entities.RefreshToken RefreshToken { get; set; }

    public RefreshedTokenCommandResponse(AccessToken accessToken, Domain.Entities.RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}