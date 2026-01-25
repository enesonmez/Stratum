using System.Collections.Immutable;
using Application.Features.Auth.Mappers;
using Core.Security.Abstractions.Jwt;
using Domain.Entities;
using Domain.Repositories.RefreshTokens;
using Domain.Repositories.UserOperationClaims;
using Microsoft.Extensions.Configuration;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IUserOperationClaimReadRepository _userOperationClaimReadRepository;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
    private readonly ITokenHelper<Guid, int, Guid> _tokenHelper;
    private readonly AuthMapper _authMapper;
    private readonly TokenOptions _tokenOptions;

    public AuthManager(
        IUserOperationClaimReadRepository userOperationClaimReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        IRefreshTokenReadRepository refreshTokenReadRepository,
        ITokenHelper<Guid, int, Guid> tokenHelper,
        AuthMapper authMapper,
        IConfiguration configuration)
    {
        _userOperationClaimReadRepository = userOperationClaimReadRepository;
        _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _refreshTokenReadRepository = refreshTokenReadRepository;
        _tokenHelper = tokenHelper;
        _authMapper = authMapper;
        
        const string tokenOptionsConfigurationSection = "TokenOptions";
        _tokenOptions =
            configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration");
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

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenWriteRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(Guid userId)
    {
        List<RefreshToken> refreshTokens = await _refreshTokenReadRepository.GetOldRefreshTokensAsync(
            userId,
            _tokenOptions.RefreshTokenTtl
        );
        await _refreshTokenWriteRepository.DeleteRangeAsync(refreshTokens);
    }
}