using Application.Features.Users.Rules;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQueryRequest, GetByIdUserQueryResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly UserBusinessRules _userBusinessRules;

    public GetByIdUserQueryHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
    {
        _userService = userService;
        _mapper = mapper;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
    {
        User? user = await _userService.GetByIdAsync(
            request.Id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
        
        return _mapper.Map<GetByIdUserQueryResponse>(user);
    }
}