using Application.Services.UserService;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQueryHandler : IRequestHandler<GetListUserQueryRequest, GetListResponse<GetListUserListItemDto>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetListUserQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQueryRequest request, CancellationToken cancellationToken)
    {
        IPaginate<User> users = await _userService.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        
        return _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
    }
}