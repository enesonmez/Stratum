using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userForRegisterDto)
        {
            RegisterCommandRequest registerCommand = new() { UserForRegisterDto = userForRegisterDto, IpAddress = GetIpAddress() };
            RegisteredCommandResponse result = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created(uri: "", result.AccessToken);
        }
        
        private string GetRefreshTokenFromCookies()
        {
            return Request.Cookies[AuthConstants.RefreshTokenCookieName] ?? throw new ArgumentException("Refresh token is not found in request cookies.");
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
            Response.Cookies.Append(key: AuthConstants.RefreshTokenCookieName, refreshToken.Token, cookieOptions);
        }
    }
}
