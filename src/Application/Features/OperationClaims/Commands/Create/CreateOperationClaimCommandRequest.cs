using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Performance;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommandRequest : IRequest<CreatedOperationClaimCommandResponse>, ILoggableRequest,
    ITransactionalRequest, IPerformanceRequest
{
    public string Name { get; set; }

    public CreateOperationClaimCommandRequest()
    {
        Name = string.Empty;
    }

    public CreateOperationClaimCommandRequest(string name)
    {
        Name = name;
    }

    int IPerformanceRequest.Interval => 0;
}