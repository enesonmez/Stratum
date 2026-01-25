using Core.CrossCuttingConcerns.Exception.Types;
using Core.Domain.Services;
using Core.Security.Abstractions.Jwt;
using Domain.Entities;
using Domain.Repositories.RefreshTokens;
using Domain.Services.RefreshTokens.Constants;

namespace Domain.Services.RefreshTokens;

public class RefreshTokenDomainService : IDomainService
{
    private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly ITokenHelper<Guid, int, Guid> _tokenHelper;

    public RefreshTokenDomainService(
        IRefreshTokenReadRepository refreshTokenReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        ITokenHelper<Guid, int, Guid> tokenHelper)
    {
        _refreshTokenReadRepository = refreshTokenReadRepository;
        _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _tokenHelper = tokenHelper;
    }

    public async Task<RefreshToken> GetByTokenAsync(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenReadRepository.GetAsync(
            predicate: r => r.Token == token,
            enableTracking: true
        );

        ValidateRefreshTokenExists(refreshToken);

        return refreshToken!;
    }

    public async Task<RefreshToken> RotateRefreshTokenAsync(User user, RefreshToken refreshToken, string ipAddress)
    {
        if (refreshToken.RevokedDate != null)
        {
            await RevokeDescendantRefreshTokensAsync(refreshToken, ipAddress, $"Attempted reuse of revoked ancestor token: {refreshToken.Token}");
            throw new BusinessException(RefreshTokensMessages.InvalidRefreshTokenReuseDetected, RefreshTokensMessages.SectionName);
        }

        if (refreshToken.ExpirationDate < DateTime.UtcNow)
        {
            throw new BusinessException(RefreshTokensMessages.RefreshTokenExpired, RefreshTokensMessages.SectionName);
        }

        var coreRefreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        RefreshToken newRefreshToken = new()
        {
            Token = coreRefreshToken.Token,
            ExpirationDate = coreRefreshToken.ExpirationDate,
            UserId = user.Id,
            CreatedByIp = ipAddress,
            CreatedDate = DateTime.UtcNow
        };

        refreshToken.Revoke(ipAddress, "Replaced by new token", newRefreshToken.Token);
        
        await _refreshTokenWriteRepository.UpdateAsync(refreshToken);
        await _refreshTokenWriteRepository.AddAsync(newRefreshToken);

        return newRefreshToken;
    }

    public async Task RevokeDescendantRefreshTokensAsync(RefreshToken refreshToken, string ipAddress, string reason)
    {
        RefreshToken? childToken = await _refreshTokenReadRepository.GetAsync(
            predicate: r => r.Token == refreshToken.ReplacedByToken
        );

        if (childToken != null && childToken.RevokedDate == null)
        {
            childToken.Revoke(ipAddress, reason);
            await _refreshTokenWriteRepository.UpdateAsync(childToken);
            await RevokeDescendantRefreshTokensAsync(childToken, ipAddress, reason);
        }
    }
    
    private void ValidateRefreshTokenExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            throw new BusinessException(RefreshTokensMessages.RefreshTokenNotFound, RefreshTokensMessages.SectionName);
    }
}