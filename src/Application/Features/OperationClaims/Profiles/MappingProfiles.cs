using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetById;
using Application.Features.OperationClaims.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim, CreateOperationClaimCommandRequest>().ReverseMap();
        CreateMap<OperationClaim, CreatedOperationClaimCommandResponse>().ReverseMap();
        CreateMap<OperationClaim, UpdateOperationClaimCommandRequest>().ReverseMap();
        CreateMap<OperationClaim, UpdatedOperationClaimCommandResponse>().ReverseMap();
        CreateMap<OperationClaim, GetByIdOperationClaimQueryResponse>().ReverseMap();
        CreateMap<OperationClaim, GetListOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<OperationClaim>, GetListResponse<GetListOperationClaimListItemDto>>().ReverseMap();
    }
}