using Application.Services.AuthService;
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Security.Abstractions.Jwt;
using Domain.Entities;
using Domain.Services.RefreshTokens;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshedTokenCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly UserDomainService _userDomainService;
    private readonly RefreshTokenDomainService _refreshTokenDomainService;

    public RefreshTokenCommandHandler(
        IAuthService authService, 
        UserDomainService userDomainService,
        RefreshTokenDomainService refreshTokenDomainService)
    {
        _authService = authService;
        _userDomainService = userDomainService;
        _refreshTokenDomainService = refreshTokenDomainService;
    }

    public async Task<RefreshedTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.RefreshToken currentToken = await _refreshTokenDomainService.GetByTokenAsync(request.RefreshToken);

        User user = await _userDomainService.GetUserByIdAsync(currentToken.UserId);

        Domain.Entities.RefreshToken newRefreshToken = await _refreshTokenDomainService.RotateRefreshTokenAsync(user, currentToken, request.IpAddress);
        AccessToken newAccessToken = await _authService.CreateAccessToken(user);
        
        await _authService.DeleteOldRefreshTokens(user.Id);

        return new RefreshedTokenCommandResponse(newAccessToken, newRefreshToken);
    }
}