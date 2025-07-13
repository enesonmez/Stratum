using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserService.Contracts;

public interface IUserService
{
    Task<User?> GetAsync(
        Expression<Func<User, bool>> predicate,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    
    Task<User> GetByIdAsync(
        Guid id,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    ); 

    Task<IPaginate<User>> GetListAsync(
        Expression<Func<User, bool>>? predicate = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    
    Task<bool> AnyAsync(
        Expression<Func<User, bool>>? predicate = null,
        bool withDeleted = false,
        CancellationToken cancellationToken = default
    );

    Task<User> AddAsync(User user);
    Task<User> CreateAsync(User user, string password);
    Task<User> UpdateAsync(User user);
    Task<User> UpdateWithPasswordAsync(User user, string password);
    Task<User> DeleteAsync(User user, bool permanent = false);
    Task<User> DeleteByIdAsync(Guid id, bool permanent = false);
}