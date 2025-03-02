using Application.Services.UserService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdatedUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<UpdatedUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        User? user = await _userService.GetAsync(u=>u.Id.Equals(request.Id), cancellationToken: cancellationToken);
        user = _mapper.Map(request, user);
        
        User updatedUser = await _userService.UpdateWithPasswordAsync(user, request.Password);
        
        return _mapper.Map<UpdatedUserCommandResponse>(updatedUser);
    }
}