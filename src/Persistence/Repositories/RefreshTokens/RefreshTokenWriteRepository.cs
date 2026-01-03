using Application.Repositories.RefreshTokens;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.RefreshTokens;

public class RefreshTokenWriteRepository : EfWriteRepositoryBase<RefreshToken, Guid, BaseDbContext>,
    IRefreshTokenWriteRepository
{
    public RefreshTokenWriteRepository(BaseDbContext context) : base(context)
    {
    }
}