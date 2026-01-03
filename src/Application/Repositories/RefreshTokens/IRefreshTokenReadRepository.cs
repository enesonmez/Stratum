using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.RefreshTokens;

public interface IRefreshTokenReadRepository : IAsyncReadRepository<RefreshToken, Guid>,
    IReadRepository<RefreshToken, Guid>
{
}