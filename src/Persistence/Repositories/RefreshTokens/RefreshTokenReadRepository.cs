using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.RefreshTokens;

public class RefreshTokenReadRepository : EfReadRepositoryBase<RefreshToken, Guid, BaseDbContext>,
    IRefreshTokenReadRepository
{
    public RefreshTokenReadRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<RefreshToken>> GetOldRefreshTokensAsync(Guid userId, int refreshTokenTtl)
    {
        List<RefreshToken> tokens = await Query()
            .AsNoTracking()
            .Where(r =>
                r.UserId == userId && 
                (
                    r.RevokedDate != null
                    || r.ExpirationDate <= DateTime.UtcNow
                    || r.CreatedDate.AddDays(refreshTokenTtl) <= DateTime.UtcNow
                )
            )
            .ToListAsync();

        return tokens;
    }
}