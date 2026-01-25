using Application.Services.AuthService;
using Core.Security.Abstractions.Jwt;
using Domain.Entities;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoggedCommandResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IAuthService _authService;

    public LoginCommandHandler(UserDomainService userDomainService, IAuthService authService)
    {
        _userDomainService = userDomainService;
        _authService = authService;
    }

    public async Task<LoggedCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await _userDomainService.VerifyUserCredentialsAsync(
            request.UserForLoginDto.Email,
            request.UserForLoginDto.Password
        );

        AccessToken accessToken = await _authService.CreateAccessToken(user);
        Domain.Entities.RefreshToken refreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
        Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(refreshToken);
        await _authService.DeleteOldRefreshTokens(user.Id);        

        return new LoggedCommandResponse
        {
            AccessToken = accessToken,
            RefreshToken = addedRefreshToken
        };
    }
}