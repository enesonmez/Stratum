using Application.Features.Users.Mappers;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;
using Domain.Repositories.Users;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQueryHandler : IRequestHandler<GetListUserQueryRequest, GetListResponse<GetListUserListItemDto>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly UserMapper _userMapper;

    public GetListUserQueryHandler(IUserReadRepository userReadRepository, UserMapper userMapper)
    {
        _userReadRepository = userReadRepository;
        _userMapper = userMapper;
    }

    public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQueryRequest request, CancellationToken cancellationToken)
    {
        IPaginate<User> users = await _userReadRepository.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        
        GetListResponse<GetListUserListItemDto> response = _userMapper.ToGetListUserResponse(users);
        return response;
    }
}