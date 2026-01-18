using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.RefreshTokens;
using Persistence.Contexts;

namespace Persistence.Repositories.RefreshTokens;

public class RefreshTokenReadRepository : EfReadRepositoryBase<RefreshToken, Guid, BaseDbContext>,
    IRefreshTokenReadRepository
{
    public RefreshTokenReadRepository(BaseDbContext context) : base(context)
    {
    }
}