using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.Users;

public interface IUserReadRepository : IAsyncReadRepository<User,Guid>, IReadRepository<User, Guid>
{
    
}