using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetById;
using Application.Features.OperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] GetByIdOperationClaimQueryRequest getByIdOperationClaimQuery)
        {
            GetByIdOperationClaimQueryResponse result = await Mediator.Send(getByIdOperationClaimQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQueryRequest getListOperationClaimQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOperationClaimListItemDto> result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedOperationClaimCommandResponse>> Add(
            [FromBody] CreateOperationClaimCommandRequest createOperationClaimCommand)
        {
            CreatedOperationClaimCommandResponse result = await Mediator.Send(createOperationClaimCommand);
            return Created(uri: "", value: result);
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedOperationClaimCommandResponse>> Add(
            [FromBody] UpdateOperationClaimCommandRequest updateOperationClaimCommand)
        {
            UpdatedOperationClaimCommandResponse result = await Mediator.Send(updateOperationClaimCommand);
            return Ok(result);
        }
        
        [HttpDelete]
        public async Task<ActionResult<DeletedOperationClaimCommandResponse>> Add(
            [FromBody] DeleteOperationClaimCommandRequest deleteOperationClaimCommand)
        {
            DeletedOperationClaimCommandResponse result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(result);
        }
    }
}