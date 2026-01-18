using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Features.Users.Mappers;

[Mapper]
public partial class UserMapper
{
    #pragma warning disable RMG020
    public partial CreatedUserCommandResponse ToCreatedUserCommandResponse(User user);

    public partial UpdatedUserCommandResponse ToUpdatedUserCommandResponse(User user);

    public partial DeletedUserCommandResponse ToDeletedUserCommandResponse(User user);

    public partial GetByIdUserQueryResponse ToGetByIdUserQueryResponse(User user);

    public partial GetListResponse<GetListUserListItemDto> ToGetListUserResponse(IPaginate<User> users);
    
    public partial GetListUserListItemDto ToGetListUserListItemDto(User user);
    #pragma warning restore RMG020
}