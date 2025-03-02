using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQueryRequest getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserListItemDto> result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserQueryRequest getByIdUserQuery)
        {
            GetByIdUserQueryResponse result = await Mediator.Send(getByIdUserQuery);
            return Ok(result);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommandRequest createUserCommandRequest)
        {
            CreatedUserCommandResponse result = await Mediator.Send(createUserCommandRequest);
            return Created(uri:"", value:result);
        }

        // PUT api/<UsersController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommandRequest updateUserCommandRequest)
        {
            UpdatedUserCommandResponse result = await Mediator.Send(updateUserCommandRequest);
            return Ok(result);
        }

        // DELETE api/<UsersController>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommandRequest deleteUserCommandRequest)
        {
            DeletedUserCommandResponse result = await Mediator.Send(deleteUserCommandRequest);
            return Ok(result);
        }
    }
}
