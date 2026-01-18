using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;
using Domain.Repositories.Users;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQueryHandler : IRequestHandler<GetListUserQueryRequest, GetListResponse<GetListUserListItemDto>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IMapper _mapper;

    public GetListUserQueryHandler(IUserReadRepository userReadRepository, IMapper mapper)
    {
        _userReadRepository = userReadRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQueryRequest request, CancellationToken cancellationToken)
    {
        IPaginate<User> users = await _userReadRepository.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        
        GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
        return response;
    }
}