using AutoMapper;
using Domain.Entities;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQueryRequest, GetByIdUserQueryResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly IMapper _mapper;

    public GetByIdUserQueryHandler(UserDomainService userDomainService, IMapper mapper)
    {
        _userDomainService = userDomainService;
        _mapper = mapper;
    }

    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
    {
        User user = await _userDomainService.GetUserByIdAsync(request.Id);

        GetByIdUserQueryResponse response = _mapper.Map<GetByIdUserQueryResponse>(user);
        return response;
    }
}