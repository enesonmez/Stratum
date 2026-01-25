using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandRequest : IRequest<RefreshedTokenCommandResponse>, ITransactionalRequest
{
    public string RefreshToken { get; set; }
    public string IpAddress { get; set; }

    public RefreshTokenCommandRequest()
    {
        RefreshToken = string.Empty;
        IpAddress = string.Empty;
    }
}