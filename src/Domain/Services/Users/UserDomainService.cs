using Domain.Entities;
using Domain.Repositories.Users; 
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Domain.Services;
using Domain.Services.Users.Constants;

namespace Domain.Services.Users;

public class UserDomainService : IDomainService
{
    private readonly IUserReadRepository _userReadRepository;

    public UserDomainService(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<User> CreateUserAsync(string firstName, string lastName, string email, string password)
    {
        await ValidateEmailIsUniqueAsync(email);

        var user = User.Create(firstName, lastName, email, password);

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