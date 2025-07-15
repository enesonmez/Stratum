using Application.Features.UserOperationClaims.Rules;
using Application.Services.UserOperationClaimService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQueryRequest,
    GetByIdUserOperationClaimQueryResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;
    private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

    public GetByIdUserOperationClaimQueryHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper,
        UserOperationClaimBusinessRules userOperationClaimBusinessRules)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
        _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
    }

    public async Task<GetByIdUserOperationClaimQueryResponse> Handle(GetByIdUserOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);

        GetByIdUserOperationClaimQueryResponse response =
            _mapper.Map<GetByIdUserOperationClaimQueryResponse>(userOperationClaim);
        return response;
    }
}