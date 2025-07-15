using System.Linq.Expressions;
using Application.Repositories.Users;
using Core.Persistence.Paging;
using Core.Security.Hashing;
using Domain.Entities;

namespace Application.Services.UserService;

public class UserManager : IUserService
{
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IUserReadRepository _userReadRepository;

    public UserManager(IUserWriteRepository userWriteRepository,
        IUserReadRepository userReadRepository)
    {
        _userWriteRepository = userWriteRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task<User?> GetAsync(Expression<Func<User, bool>> predicate,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        User? user = await _userReadRepository.GetAsync(
            predicate: predicate,
            withDeleted: withDeleted,
            enableTracking: enableTracking,
            cancellationToken: cancellationToken);
        return user;
    }

    public async Task<User?> GetByIdAsync(Guid id, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        User? user = await GetAsync(u => u.Id.Equals(id), withDeleted: withDeleted, enableTracking: enableTracking,
            cancellationToken: cancellationToken);

        return user;
    }

    public async Task<IPaginate<User>> GetListAsync(Expression<Func<User, bool>>? predicate = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IPaginate<User> userList = await _userReadRepository.GetListAsync(
            predicate: predicate,
            orderBy: orderBy,
            index: index,
            size: size,
            withDeleted: withDeleted,
            enableTracking: enableTracking,
            cancellationToken: cancellationToken
        );
        return userList;
    }

    public async Task<bool> AnyAsync(
        Expression<Func<User, bool>>? predicate = null,
        bool withDeleted = false,
        CancellationToken cancellationToken = default
    )
    {
        return await _userReadRepository.AnyAsync(
            predicate: predicate,
            withDeleted: withDeleted,
            cancellationToken: cancellationToken
        );
    }

    public async Task<User> AddAsync(User user)
    {
        User addedUser = await _userWriteRepository.AddAsync(user);

        return addedUser;
    }

    public async Task<User> CreateAsync(User user, string password)
    {
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        User createdUser = await _userWriteRepository.AddAsync(user);

        return createdUser;
    }

    public async Task<User> UpdateAsync(User user)
    {
        User updatedUser = await _userWriteRepository.UpdateAsync(user);

        return updatedUser;
    }

    public async Task<User> UpdateWithPasswordAsync(User user, string password)
    {
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return await UpdateAsync(user);
    }

    public async Task<User> DeleteAsync(User user, bool permanent = false)
    {
        User deletedUser = await _userWriteRepository.DeleteAsync(user, permanent);

        return deletedUser;
    }
}