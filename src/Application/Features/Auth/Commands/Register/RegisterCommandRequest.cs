using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandRequest: IRequest<RegisteredCommandResponse>, ITransactionalRequest
{
    public RegisterUserDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public RegisterCommandRequest()
    {
        UserForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public RegisterCommandRequest(RegisterUserDto userForRegisterDto, string ipAddress)
    {
        UserForRegisterDto = userForRegisterDto;
        IpAddress = ipAddress;
    }
}