using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommandRequest>().ReverseMap();
        CreateMap<User, CreatedUserCommandResponse>().ReverseMap();
        CreateMap<User, DeleteUserCommandRequest>().ReverseMap();
        CreateMap<User, DeletedUserCommandResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommandRequest>().ReverseMap();
        CreateMap<User, UpdatedUserCommandResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
        CreateMap<User, GetByIdUserQueryResponse>().ReverseMap();
    }
}