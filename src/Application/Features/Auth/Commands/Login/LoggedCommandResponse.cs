using Core.Application.Responses;
using Core.Security.Abstractions.Enums;
using Core.Security.Abstractions.Jwt;
using Domain.Entities;

namespace Application.Features.Auth.Commands.Login;

public class LoggedCommandResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
    public AuthenticatorType RequiredAuthenticatorType { get; set; }

    public LoggedCommandResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public LoggedCommandResponse(AccessToken accessToken, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    
    public LoggedHttpResponse ToHttpResponse()
    {
        return new() { AccessToken = AccessToken, RequiredAuthenticatorType = RequiredAuthenticatorType };
    }

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    }
}