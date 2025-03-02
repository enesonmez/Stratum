using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
