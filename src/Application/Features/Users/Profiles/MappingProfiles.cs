using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using AutoMapper;
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
    }
}