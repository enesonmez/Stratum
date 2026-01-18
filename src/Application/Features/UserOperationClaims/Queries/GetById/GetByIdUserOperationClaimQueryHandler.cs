using AutoMapper;
using Domain.Entities;
using Domain.Services.UserOperationClaims;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQueryRequest,
    GetByIdUserOperationClaimQueryResponse>
{
    private readonly UserOperationClaimDomainService _userOperationClaimDomainService;
    private readonly IMapper _mapper;

    public GetByIdUserOperationClaimQueryHandler(
        UserOperationClaimDomainService userOperationClaimDomainService,
        IMapper mapper)
    {
        _userOperationClaimDomainService = userOperationClaimDomainService;
        _mapper = mapper;
    }

    public async Task<GetByIdUserOperationClaimQueryResponse> Handle(GetByIdUserOperationClaimQueryRequest request,
        CancellationToken cancellationToken)
    {
        UserOperationClaim userOperationClaim = await _userOperationClaimDomainService.GetUserOperationClaimByIdAsync(request.Id);

        GetByIdUserOperationClaimQueryResponse response =
            _mapper.Map<GetByIdUserOperationClaimQueryResponse>(userOperationClaim);
        return response;
    }
}