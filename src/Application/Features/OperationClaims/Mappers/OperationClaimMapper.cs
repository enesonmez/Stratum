using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetById;
using Application.Features.OperationClaims.Queries.GetList;
using Core.Application.Responses;
using Core.Persistence.Abstractions.Paging;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Features.OperationClaims.Mappers;

[Mapper]
public partial class OperationClaimMapper
{
    #pragma warning disable RMG020
    public partial CreatedOperationClaimCommandResponse ToCreatedOperationClaimResponse(OperationClaim operationClaim);
    
    public partial UpdatedOperationClaimCommandResponse ToUpdatedOperationClaimResponse(OperationClaim operationClaim);

    public partial DeletedOperationClaimCommandResponse ToDeletedOperationClaimResponse(OperationClaim operationClaim);

    public partial GetByIdOperationClaimQueryResponse ToGetByIdOperationClaimResponse(OperationClaim operationClaim);

    public partial GetListResponse<GetListOperationClaimListItemDto> ToGetListOperationClaimResponse(IPaginate<OperationClaim> operationClaims);

    public partial GetListOperationClaimListItemDto ToGetListOperationClaimListItemDto(OperationClaim operationClaim);
    #pragma warning restore RMG020
}