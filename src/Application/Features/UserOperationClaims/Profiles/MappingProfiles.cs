using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;

namespace Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserOperationClaim, CreateUserOperationClaimCommandRequest>().ReverseMap();
        CreateMap<UserOperationClaim, CreatedUserOperationClaimCommandResponse>().ReverseMap();
        CreateMap<UserOperationClaim, DeleteUserOperationClaimCommandRequest>().ReverseMap();
        CreateMap<UserOperationClaim, DeletedUserOperationClaimCommandResponse>().ReverseMap();
        CreateMap<UserOperationClaim, GetByIdUserOperationClaimQueryResponse>().ReverseMap();
        CreateMap<UserOperationClaim, GetListUserOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<UserOperationClaim>, GetListResponse<GetListUserOperationClaimListItemDto>>().ReverseMap();
    }
}