using Application.Features.Users.Constants;
using Application.Repositories.Users;
using Core.Application.Rules;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;


    public UserBusinessRules(IUserReadRepository userReadRepository, ILocalizationService localizationService) : 
        base(localizationService)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool doesExists = await _userReadRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await ThrowBusinessException(UsersMessages.UserMailAlreadyExists, UsersMessages.SectionName);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await ThrowBusinessException(UsersMessages.UserDontExists, UsersMessages.SectionName);
    }

    public async Task UserEmailShouldNotExistsWhenUpdate(Guid id, string email)
    {
        bool doesExists = await _userReadRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email);
        if (doesExists)
            await ThrowBusinessException(UsersMessages.UserMailAlreadyExists, UsersMessages.SectionName);
    }
}