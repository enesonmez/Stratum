using Core.Persistence.Abstractions.Repositories;
using Domain.Entities;

namespace Domain.Repositories.Users;

public interface IUserWriteRepository : IAsyncWriteRepository<User,Guid>, IWriteRepository<User, Guid>
{
    
}