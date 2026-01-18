using Application.Features.Users.Mappers;
using Domain.Entities;
using Domain.Services.Users;
using MediatR;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQueryRequest, GetByIdUserQueryResponse>
{
    private readonly UserDomainService _userDomainService;
    private readonly UserMapper _userMapper;

    public GetByIdUserQueryHandler(UserDomainService userDomainService, UserMapper userMapper)
    {
        _userDomainService = userDomainService;
        _userMapper = userMapper;
    }

    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
    {
        User user = await _userDomainService.GetUserByIdAsync(request.Id);

        GetByIdUserQueryResponse response = _userMapper.ToGetByIdUserQueryResponse(user);
        return response;
    }
}