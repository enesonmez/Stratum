using AutoMapper;
using Domain.Entities;
using Domain.Repositories.OperationClaims;
using Domain.Services.OperationClaims;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Update;

public class
    UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommandRequest,
    UpdatedOperationClaimCommandResponse>
{
    private readonly OperationClaimDomainService _operationClaimDomainService;
    private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;
    private readonly IMapper _mapper;

    public UpdateOperationClaimCommandHandler(
        OperationClaimDomainService operationClaimDomainService,
        IOperationClaimWriteRepository operationClaimWriteRepository,
        IMapper mapper)
    {
        _operationClaimDomainService = operationClaimDomainService;
        _operationClaimWriteRepository = operationClaimWriteRepository;
        _mapper = mapper;
    }

    public async Task<UpdatedOperationClaimCommandResponse> Handle(UpdateOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        OperationClaim updatedOperationClaim = await _operationClaimDomainService.UpdateOperationClaimAsync(
            request.Id, 
            request.Name
        );

        await _operationClaimWriteRepository.UpdateAsync(updatedOperationClaim, cancellationToken);

        // 3. Mapping & Response
        UpdatedOperationClaimCommandResponse response = _mapper.Map<UpdatedOperationClaimCommandResponse>(updatedOperationClaim);
        return response;
    }
}