using Application.Features.UserOperationClaims.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserOperationClaim, GetByIdUserOperationClaimQueryResponse>().ReverseMap();
    }
}