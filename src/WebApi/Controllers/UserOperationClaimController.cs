using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserOperationClaimQueryRequest getListUserOperationClaimQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserOperationClaimListItemDto> result =
                await Mediator.Send(getListUserOperationClaimQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] GetByIdUserOperationClaimQueryRequest getByIdUserOperationClaimQuery)
        {
            GetByIdUserOperationClaimQueryResponse result = await Mediator.Send(getByIdUserOperationClaimQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedUserOperationClaimCommandResponse>> Add(
            [FromBody] CreateUserOperationClaimCommandRequest createUserOperationClaimCommandRequest)
        {
            CreatedUserOperationClaimCommandResponse result =
                await Mediator.Send(createUserOperationClaimCommandRequest);
            return Created(uri: "", value: result);
        }
    }
}