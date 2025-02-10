using Application.Features.Users.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommandRequest>().ReverseMap();
        CreateMap<User, CreatedUserCommandResponse>().ReverseMap();
    }
}