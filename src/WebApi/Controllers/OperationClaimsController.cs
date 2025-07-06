using Application.Features.OperationClaims.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimQueryRequest getByIdOperationClaimQuery)
        {
            GetByIdOperationClaimQueryResponse result = await Mediator.Send(getByIdOperationClaimQuery);
            return Ok(result);
        }
    }
}
