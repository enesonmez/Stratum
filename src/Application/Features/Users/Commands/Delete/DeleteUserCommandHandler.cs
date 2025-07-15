using Application.Features.Users.Rules;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeletedUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly UserBusinessRules _userBusinessRules;

    public DeleteUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
    {
        _userService = userService;
        _mapper = mapper;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<DeletedUserCommandResponse> Handle(DeleteUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        User? user = await _userService.GetByIdAsync(
                request.Id, 
                enableTracking: false, 
                cancellationToken: cancellationToken
        );
        await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

        User deletedUser = await _userService.DeleteAsync(user!);
        return _mapper.Map<DeletedUserCommandResponse>(deletedUser);
    }
}