using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
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

        [HttpPut]
        public async Task<ActionResult<UpdatedUserOperationClaimCommandResponse>> Put(
            [FromBody] UpdateUserOperationClaimCommandRequest updateUserOperationClaimCommandRequest)
        {
            UpdatedUserOperationClaimCommandResponse result =
                await Mediator.Send(updateUserOperationClaimCommandRequest);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<DeletedUserOperationClaimCommandResponse>> Delete(
            [FromBody] DeleteUserOperationClaimCommandRequest deleteUserOperationClaimCommandRequest)
        {
            DeletedUserOperationClaimCommandResponse result =
                await Mediator.Send(deleteUserOperationClaimCommandRequest);
            return Ok(result);
        }
    }
}