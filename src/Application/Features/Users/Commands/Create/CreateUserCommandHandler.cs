using Application.Features.Users.Rules;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreatedUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly UserBusinessRules _userBusinessRules;

    public CreateUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
    {
        _userService = userService;
        _mapper = mapper;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<CreatedUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(request.Email);
        User user = _mapper.Map<User>(request);
        User createdUser = await _userService.CreateAsync(user, request.Password);
        
        return _mapper.Map<CreatedUserCommandResponse>(createdUser);
    }
}