using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
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
            GetListUserOperationClaimQueryRequest getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserOperationClaimListItemDto> result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }
        
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserOperationClaimQueryRequest getByIdUserQuery)
        {
            GetByIdUserOperationClaimQueryResponse result = await Mediator.Send(getByIdUserQuery);
            return Ok(result);
        }
    }
}
