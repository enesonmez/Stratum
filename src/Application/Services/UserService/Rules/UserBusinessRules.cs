using Application.Features.Users.Constants;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exception.Types;
using Core.Localization.Abstraction;

namespace Application.Services.UserService.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly ILocalizationService _localizationService;

    public UserBusinessRules(IUserRepository userRepository, ILocalizationService localizationService)
    {
        _userRepository = userRepository;
        _localizationService = localizationService;
    }
    
    private async Task ThrowBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UsersMessages.SectionName);
        throw new BusinessException(message);
    }
    
    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await ThrowBusinessException(UsersMessages.UserMailAlreadyExists);
    }
}