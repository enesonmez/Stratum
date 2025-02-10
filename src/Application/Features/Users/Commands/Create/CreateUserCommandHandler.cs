using Application.Services.UserService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreatedUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<CreatedUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        User user = _mapper.Map<User>(request);
        User createdUser = await _userService.CreateAsync(user, request.Password);
        
        return _mapper.Map<CreatedUserCommandResponse>(createdUser);
    }
}