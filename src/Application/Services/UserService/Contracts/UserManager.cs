using System.Linq.Expressions;
using Application.Repositories.Users;
using Application.Services.UserService.Rules;
using Core.Persistence.Paging;
using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserService.Contracts;

public class UserManager : IUserService
{
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly UserBusinessRules _userBusinessRules;
    
    public UserManager(IUserWriteRepository userWriteRepository, 
        IUserReadRepository userReadRepository, 
        UserBusinessRules userBusinessRules)
    {
        _userWriteRepository = userWriteRepository;
        _userReadRepository = userReadRepository;
        _userBusinessRules = userBusinessRules;
    }
    
    public async Task<User?> GetAsync(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        User? user = await _userReadRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return user;
    }

    public async Task<User> GetByIdAsync(Guid id, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        User? user = await GetAsync(u=>u.Id.Equals(id), withDeleted: withDeleted, enableTracking: enableTracking, cancellationToken:cancellationToken);
        
        await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

        return user!;
    }

    public async Task<IPaginate<User>> GetListAsync(Expression<Func<User, bool>>? predicate = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<User> userList = await _userReadRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userList;
    }

    public async Task<User> AddAsync(User user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(user.Email);
        User addedUser = await _userWriteRepository.AddAsync(user);
        
        return addedUser;
    }

    public async Task<User> CreateAsync(User user, string password)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(user.Email);
        
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        User createdUser = await _userWriteRepository.AddAsync(user);
        
        return createdUser;
    }
    
    public async Task<User> UpdateAsync(User user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user.Id, user.Email);
        
        User updatedUser = await _userWriteRepository.UpdateAsync(user);
        
        return updatedUser;
    }

    public async Task<User> UpdateWithPasswordAsync(User user, string password)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);
        
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        User updatedUser = await _userWriteRepository.UpdateAsync(user);
        
        return updatedUser;
    }

    public async Task<User> DeleteAsync(User user, bool permanent = false)
    {
        User deletedUser = await _userWriteRepository.DeleteAsync(user, permanent);

        return deletedUser;
    }
    
    public async Task<User> DeleteByIdAsync(Guid id, bool permanent = false)
    {
        User? user = await _userReadRepository.GetAsync(
            predicate: u => u.Id.Equals(id)
        );
        
        await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

        return await DeleteAsync(user!, permanent);
    }
}