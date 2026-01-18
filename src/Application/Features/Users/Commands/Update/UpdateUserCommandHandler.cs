using AutoMapper;
using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdatedUserCommandResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(
        UserDomainService userDomainService, 
        IUserWriteRepository userWriteRepository, 
        IMapper mapper)
    {
        _userDomainService = userDomainService;
        _userWriteRepository = userWriteRepository;
        _mapper = mapper;
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

        UpdatedUserCommandResponse response = _mapper.Map<UpdatedUserCommandResponse>(updatedUser);
        return response;
    }
}