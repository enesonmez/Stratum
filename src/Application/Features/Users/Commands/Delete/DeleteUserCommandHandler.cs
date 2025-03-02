using Application.Services.UserService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeletedUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<DeletedUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
    {
        User deletedUser = await _userService.DeleteByIdAsync(request.Id);
        return _mapper.Map<DeletedUserCommandResponse>(deletedUser);
    }
}