using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories.Users;

public interface IUserWriteRepository : IAsyncWriteRepository<User,Guid>, IWriteRepository<User, Guid>
{
    
}