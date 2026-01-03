using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.RefreshTokens;

public interface IRefreshTokenWriteRepository : IAsyncWriteRepository<RefreshToken, Guid>,
    IWriteRepository<RefreshToken, Guid>
{
}