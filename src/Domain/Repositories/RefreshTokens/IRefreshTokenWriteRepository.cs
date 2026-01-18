using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.RefreshTokens;

public interface IRefreshTokenWriteRepository : IAsyncWriteRepository<RefreshToken, Guid>,
    IWriteRepository<RefreshToken, Guid>
{
}