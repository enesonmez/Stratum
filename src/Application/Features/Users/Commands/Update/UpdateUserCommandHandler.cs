using Application.Features.Users.Rules;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdatedUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly UserBusinessRules _userBusinessRules;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
    {
        _userService = userService;
        _userBusinessRules = userBusinessRules;
        _mapper = mapper;
    }

    public async Task<UpdatedUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        User? user = await _userService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
        await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, request.Email);
        
        user = _mapper.Map(request, user);
        
        User updatedUser = await _userService.UpdateAsync(user);
        
        return _mapper.Map<UpdatedUserCommandResponse>(updatedUser);
    }
}