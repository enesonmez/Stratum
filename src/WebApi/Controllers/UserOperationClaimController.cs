using Application.Features.UserOperationClaims.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserOperationClaimQueryRequest getByIdUserQuery)
        {
            GetByIdUserOperationClaimQueryResponse result = await Mediator.Send(getByIdUserQuery);
            return Ok(result);
        }
    }
}
