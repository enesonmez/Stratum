using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommandRequest : IRequest<UpdatedOperationClaimCommandResponse>, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdateOperationClaimCommandRequest()
    {
        Name = string.Empty;
    }

    public UpdateOperationClaimCommandRequest(string name, int id)
    {
        Name = name;
        Id = id;
    }
}