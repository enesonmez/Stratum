using AutoMapper;
using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreatedUserCommandResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        UserDomainService userDomainService, 
        IUserWriteRepository userWriteRepository, 
        IMapper mapper)
    {
        _userDomainService = userDomainService;
        _userWriteRepository = userWriteRepository;
        _mapper = mapper;
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
        
        CreatedUserCommandResponse response = _mapper.Map<CreatedUserCommandResponse>(createdUser);
        return response;
    }
}