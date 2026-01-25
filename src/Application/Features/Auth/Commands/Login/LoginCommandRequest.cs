using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommandRequest : IRequest<LoggedCommandResponse>, ITransactionalRequest
{
    public LoginUserDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public LoginCommandRequest()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public LoginCommandRequest(LoginUserDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }
}