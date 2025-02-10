using System.Linq.Expressions;
using Application.Repositories;
using Application.Services.UserService.Rules;
using Core.Persistence.Paging;
using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserService.Contracts;

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserBusinessRules _userBusinessRules;
    
    public UserManager(IUserRepository userRepository, UserBusinessRules userBusinessRules)
    {
        _userRepository = userRepository;
        _userBusinessRules = userBusinessRules;
    }
    
    public Task<User?> GetAsync(Expression<Func<User, bool>> predicate, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IPaginate<User>?> GetListAsync(Expression<Func<User, bool>>? predicate = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> AddAsync(User user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(user.Email);
        User addedUser = await _userRepository.AddAsync(user);
        
        return addedUser;
    }

    public async Task<User> CreateAsync(User user, string password)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(user.Email);
        
        HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        User createdUser = await _userRepository.AddAsync(user);
        
        return createdUser;
    }

    public Task<User> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(User user, bool permanent = false)
    {
        throw new NotImplementedException();
    }
}