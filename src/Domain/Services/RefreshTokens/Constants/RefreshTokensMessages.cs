namespace Domain.Services.RefreshTokens.Constants;

public class RefreshTokensMessages
{
    public const string SectionName = "refresh-tokens";

    public const string RefreshTokenNotFound = "RefreshTokenNotFound";
    public const string RefreshTokenExpired = "RefreshTokenExpired";
    public const string InvalidRefreshTokenReuseDetected = "InvalidRefreshTokenReuseDetected";
}