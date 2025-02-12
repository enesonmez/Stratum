using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.Users;

public interface IUserReadRepository : IAsyncReadRepository<User,Guid>, IReadRepository<User, Guid>
{
    
}