using Application.Services.OperationClaimService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Update;

public class
    UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommandRequest,
    UpdatedOperationClaimCommandResponse>
{
    private readonly IOperationClaimService _operationClaimService;
    private readonly IMapper _mapper;

    public UpdateOperationClaimCommandHandler(IMapper mapper, IOperationClaimService operationClaimService)
    {
        _mapper = mapper;
        _operationClaimService = operationClaimService;
    }

    public async Task<UpdatedOperationClaimCommandResponse> Handle(UpdateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim operationClaim =
            await _operationClaimService.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        OperationClaim mappedOperationClaim = _mapper.Map(request, operationClaim);
        OperationClaim updatedOperationClaim = await _operationClaimService.UpdateAsync(mappedOperationClaim);

        UpdatedOperationClaimCommandResponse response =
            _mapper.Map<UpdatedOperationClaimCommandResponse>(updatedOperationClaim);
        return response;
    }
}