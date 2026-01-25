using Domain.Entities;
using Domain.Repositories.Users; 
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Domain.Services;
using Core.Security.Abstractions.Hashing;
using Domain.Services.Users.Constants;

namespace Domain.Services.Users;

public class UserDomainService : IDomainService
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IHashingService _hashingService;

    public UserDomainService(IUserReadRepository userReadRepository, IHashingService hashingService)
    {
        _userReadRepository = userReadRepository;
        _hashingService = hashingService;
    }

    public async Task<User> CreateUserAsync(string firstName, string lastName, string email, string password)
    {
        await ValidateEmailIsUniqueAsync(email);
        
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            throw new BusinessException(UsersMessages.PasswordTooWeak, UsersMessages.SectionName);
        }
        
        _hashingService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = User.Create(firstName, lastName, email, passwordHash, passwordSalt);

        return user;
    }
    
    public async Task<User> DeleteUserAsync(Guid id)
    {
        User? user = await _userReadRepository.GetAsync(
            predicate: u => u.Id == id);

        ValidateUserExists(user);

        user!.Delete(); 

        return user;
    }
    
    public async Task<User> UpdateUserAsync(Guid id, string firstName, string lastName, string email)
    {
        User? user = await _userReadRepository.GetAsync(
            predicate: u => u.Id == id);

        ValidateUserExists(user);

        await ValidateEmailIsUniqueForUpdateAsync(user!, email);

        user!.Update(firstName, lastName, email);

        return user;
    }
    
    public async Task<User> GetUserByIdAsync(Guid id)
    {
        User? user = await _userReadRepository.GetAsync(
            predicate: u => u.Id == id,
            enableTracking: false);

        ValidateUserExists(user);

        return user!;
    }
    
    public async Task<User> VerifyUserCredentialsAsync(string email, string password)
    {
        User? user = await _userReadRepository.GetAsync(
            predicate: u => u.Email == email,
            enableTracking: false 
        );

        ValidateUserExists(user);

        bool isPasswordValid = _hashingService.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt);
        
        if (!isPasswordValid)
        {
            throw new BusinessException(UsersMessages.PasswordDontMatch, UsersMessages.SectionName);
        }

        return user;
    }
    
    // --- Private Helper Methods ---
    private void ValidateUserExists(User? user)
    {
        if (user == null)
        {
            throw new BusinessException(UsersMessages.UserNotFound, UsersMessages.SectionName);
        }
    }
    
    private async Task ValidateEmailIsUniqueForUpdateAsync(User user, string newEmail)
    {
        if (user.Email == newEmail) 
            return;
        
        await ValidateEmailIsUniqueAsync(newEmail);
    }
    
    private async Task ValidateEmailIsUniqueAsync(string email)
    {
        bool doesExists = await _userReadRepository.AnyAsync(
            predicate: u => u.Email == email);
            
        if (doesExists)
            throw new BusinessException(UsersMessages.UserMailAlreadyExists, UsersMessages.SectionName);
    }
}