using Application.Features.Users.Mappers;
using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreatedUserCommandResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly UserMapper _userMapper;

    public CreateUserCommandHandler(
        UserDomainService userDomainService, 
        IUserWriteRepository userWriteRepository, 
        UserMapper userMapper)
    {
        _userDomainService = userDomainService;
        _userWriteRepository = userWriteRepository;
        _userMapper = userMapper;
    }

    public async Task<CreatedUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        User createdUser = await _userDomainService.CreateUserAsync(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        
        await _userWriteRepository.AddAsync(createdUser, cancellationToken);

        CreatedUserCommandResponse response = _userMapper.ToCreatedUserCommandResponse(createdUser);
        return response;
    }
}