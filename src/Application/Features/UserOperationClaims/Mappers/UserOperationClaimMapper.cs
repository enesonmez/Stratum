using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Features.UserOperationClaims.Mappers;

[Mapper]
public partial class UserOperationClaimMapper
{
    #pragma warning disable RMG020
    public partial CreatedUserOperationClaimCommandResponse ToCreatedUserOperationClaimResponse(UserOperationClaim userOperationClaim);

    public partial DeletedUserOperationClaimCommandResponse ToDeletedUserOperationClaimResponse(UserOperationClaim userOperationClaim);

    public partial GetByIdUserOperationClaimQueryResponse ToGetByIdUserOperationClaimResponse(UserOperationClaim userOperationClaim);

    public partial GetListResponse<GetListUserOperationClaimListItemDto> ToGetListUserOperationClaimResponse(IPaginate<UserOperationClaim> userOperationClaims);

    public partial GetListUserOperationClaimListItemDto ToGetListUserOperationClaimListItemDto(UserOperationClaim userOperationClaim);
    #pragma warning restore RMG020
}