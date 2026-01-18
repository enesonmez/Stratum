using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.RefreshTokens;

public interface IRefreshTokenReadRepository : IAsyncReadRepository<RefreshToken, Guid>,
    IReadRepository<RefreshToken, Guid>
{
}