using System.Collections.Immutable;
using Application.Features.Auth.Mappers;
using Core.Security.Abstractions.Jwt;
using Domain.Entities;
using Domain.Repositories.RefreshTokens;
using Domain.Repositories.UserOperationClaims;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IUserOperationClaimReadRepository _userOperationClaimReadRepository;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly ITokenHelper<Guid, int, Guid> _tokenHelper;
    private readonly AuthMapper _authMapper;

    public AuthManager(
        IUserOperationClaimReadRepository userOperationClaimReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        ITokenHelper<Guid, int, Guid> tokenHelper,
        AuthMapper authMapper)
    {
        _userOperationClaimReadRepository = userOperationClaimReadRepository;
        _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _tokenHelper = tokenHelper;
        _authMapper = authMapper;
    }
    
    public async Task<AccessToken> CreateAccessToken(User user)
    {
        IList<OperationClaim> operationClaims = await _userOperationClaimReadRepository.GetOperationClaimsByUserIdAsync(user.Id);
        AccessToken accessToken = _tokenHelper.CreateToken(
            user,
            operationClaims.Select(op => (Core.Security.Abstractions.Entities.OperationClaim<int>)op).ToImmutableList()
        );
        return accessToken;
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        Core.Security.Abstractions.Entities.RefreshToken<Guid, Guid> coreRefreshToken = _tokenHelper.CreateRefreshToken(
            user,
            ipAddress
        );
        RefreshToken refreshToken = _authMapper.ToRefreshToken(coreRefreshToken);
        return Task.FromResult(refreshToken);
    }

    public Task<RefreshToken?> GetRefreshTokenByToken(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenWriteRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public Task DeleteOldRefreshTokens(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
    {
        throw new NotImplementedException();
    }

    public Task RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
    {
        throw new NotImplementedException();
    }
}