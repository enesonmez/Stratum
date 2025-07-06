using Application.Features.OperationClaims.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim, GetByIdOperationClaimQueryResponse>().ReverseMap();
    }
}