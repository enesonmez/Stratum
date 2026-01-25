using Application.Services.AuthService;
using Core.Security.Abstractions.Jwt;
using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisteredCommandResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IAuthService _authService;

    public RegisterCommandHandler(
        UserDomainService userDomainService,
        IUserWriteRepository userWriteRepository, 
        IAuthService authService
    )
    {
        _userDomainService = userDomainService;
        _userWriteRepository = userWriteRepository;
        _authService = authService;
    }
    
    public async Task<RegisteredCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        User newUser = await _userDomainService.CreateUserAsync(
            request.UserForRegisterDto.FirstName,
            request.UserForRegisterDto.LastName,
            request.UserForRegisterDto.Email,
            request.UserForRegisterDto.Password
        );
        
        User createdUser = await _userWriteRepository.AddAsync(newUser, cancellationToken);
        
        AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
        
        Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
            createdUser,
            request.IpAddress
        );
        Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

        RegisteredCommandResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
        return registeredResponse;
    }
}