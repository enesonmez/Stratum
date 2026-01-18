using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.RefreshTokens;
using Persistence.Contexts;

namespace Persistence.Repositories.RefreshTokens;

public class RefreshTokenWriteRepository : EfWriteRepositoryBase<RefreshToken, Guid, BaseDbContext>,
    IRefreshTokenWriteRepository
{
    public RefreshTokenWriteRepository(BaseDbContext context) : base(context)
    {
    }
}