using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories.Users;
using Persistence.Contexts;

namespace Persistence.Repositories.Users;

public class UserReadRepository : EfReadRepositoryBase<User, Guid, BaseDbContext>, IUserReadRepository
{
    public UserReadRepository(BaseDbContext context)
        : base(context) { }
}