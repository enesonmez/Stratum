using Application.Features.Users.Mappers;
using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdatedUserCommandResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly UserMapper _userMapper;

    public UpdateUserCommandHandler(
        UserDomainService userDomainService, 
        IUserWriteRepository userWriteRepository, 
        UserMapper userMapper)
    {
        _userDomainService = userDomainService;
        _userWriteRepository = userWriteRepository;
        _userMapper = userMapper;
    }

    public async Task<UpdatedUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        User updatedUser = await _userDomainService.UpdateUserAsync(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email
        );

        await _userWriteRepository.UpdateAsync(updatedUser, cancellationToken);

        UpdatedUserCommandResponse response = _userMapper.ToUpdatedUserCommandResponse(updatedUser);
        return response;
    }
}