using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.Users;
using Persistence.Contexts;

namespace Persistence.Repositories.Users;

public class UserWriteRepository : EfWriteRepositoryBase<User, Guid, BaseDbContext>, IUserWriteRepository
{
    public UserWriteRepository(BaseDbContext context) 
        : base(context)
    {
    }
}