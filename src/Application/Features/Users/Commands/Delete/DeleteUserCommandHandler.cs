using Application.Features.Users.Mappers;
using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeletedUserCommandResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly UserMapper _userMapper;

    public DeleteUserCommandHandler(
        UserDomainService userDomainService,
        IUserWriteRepository userWriteRepository, 
        UserMapper userMapper)
    {
        _userDomainService = userDomainService;
        _userWriteRepository = userWriteRepository;
        _userMapper = userMapper;
    }

    public async Task<DeletedUserCommandResponse> Handle(DeleteUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        User userToDelete = await _userDomainService.DeleteUserAsync(request.Id);

        User deletedUser = await _userWriteRepository.DeleteAsync(userToDelete, cancellationToken: cancellationToken);
        
        DeletedUserCommandResponse response = _userMapper.ToDeletedUserCommandResponse(deletedUser);
        return response;
    }
}