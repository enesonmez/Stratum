using Application.Features.Users.Constants;
using Application.Repositories.Users;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Services.UserService.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly ILocalizationService _localizationService;

    public UserBusinessRules(IUserReadRepository userReadRepository, ILocalizationService localizationService)
    {
        _userReadRepository = userReadRepository;
        _localizationService = localizationService;
    }
    
    private async Task ThrowBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UsersMessages.SectionName);
        throw new BusinessException(message);
    }
    
    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool doesExists = await _userReadRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await ThrowBusinessException(UsersMessages.UserMailAlreadyExists);
    }
    
    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await ThrowBusinessException(UsersMessages.UserDontExists);
    }
    
    public async Task UserEmailShouldNotExistsWhenUpdate(Guid id, string email)
    {
        bool doesExists = await _userReadRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email);
        if (doesExists)
            await ThrowBusinessException(UsersMessages.UserMailAlreadyExists);
    }
}