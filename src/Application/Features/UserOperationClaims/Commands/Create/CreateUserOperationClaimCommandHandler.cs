using AutoMapper;
using Domain.Entities;
using Domain.Repositories.UserOperationClaims;
using Domain.Services.UserOperationClaims;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommandRequest,
    CreatedUserOperationClaimCommandResponse>
{
    private readonly UserOperationClaimDomainService _userOperationClaimDomainService;
    private readonly IUserOperationClaimWriteRepository _userOperationClaimWriteRepository;
    private readonly IMapper _mapper;

    public CreateUserOperationClaimCommandHandler(
        UserOperationClaimDomainService userOperationClaimDomainService,
        IUserOperationClaimWriteRepository userOperationClaimWriteRepository,
        IMapper mapper)
    {
        _userOperationClaimDomainService = userOperationClaimDomainService;
        _userOperationClaimWriteRepository = userOperationClaimWriteRepository;
        _mapper = mapper;
    }

    public async Task<CreatedUserOperationClaimCommandResponse> Handle(CreateUserOperationClaimCommandRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim userOperationClaim = await _userOperationClaimDomainService.CreateUserOperationClaimAsync(
            request.UserId, 
            request.OperationClaimId
        );

        await _userOperationClaimWriteRepository.AddAsync(userOperationClaim, cancellationToken);

        CreatedUserOperationClaimCommandResponse response = _mapper.Map<CreatedUserOperationClaimCommandResponse>(userOperationClaim);
        return response;
    }
}