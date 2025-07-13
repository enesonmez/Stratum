using Application.Services.UserOperationClaimService.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQueryRequest,
    GetByIdUserOperationClaimQueryResponse>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IMapper _mapper;

    public GetByIdUserOperationClaimQueryHandler(IUserOperationClaimService userOperationClaimService, IMapper mapper)
    {
        _userOperationClaimService = userOperationClaimService;
        _mapper = mapper;
    }

    public async Task<GetByIdUserOperationClaimQueryResponse> Handle(GetByIdUserOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim userOperationClaim =
            await _userOperationClaimService.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        GetByIdUserOperationClaimQueryResponse response =
            _mapper.Map<GetByIdUserOperationClaimQueryResponse>(userOperationClaim);
        return response;
    }
}