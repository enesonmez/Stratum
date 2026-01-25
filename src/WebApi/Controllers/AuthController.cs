using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.RefreshToken;
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
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userForLoginDto)
        {
            LoginCommandRequest loginCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = GetIpAddress() };
            LoggedCommandResponse result = await Mediator.Send(loginCommand);
            SetRefreshTokenToCookie(result.RefreshToken);

            return Ok(result.ToHttpResponse());
        }
        
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            string refreshToken = GetRefreshTokenFromCookies();
            RefreshTokenCommandRequest refreshTokenCommandRequest = new() 
            { 
                RefreshToken = refreshToken, 
                IpAddress = GetIpAddress() 
            };

            RefreshedTokenCommandResponse result = await Mediator.Send(refreshTokenCommandRequest);
            SetRefreshTokenToCookie(result.RefreshToken);

            return Ok(result.AccessToken);
        }
        
        private string GetRefreshTokenFromCookies()
        {
            return Request.Cookies[AuthConstants.RefreshTokenCookieName] ?? throw new ArgumentException("Refresh token is not found in request cookies.");
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true, 
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite =  SameSiteMode.Strict
            };
            Response.Cookies.Append(key: AuthConstants.RefreshTokenCookieName, refreshToken.Token, cookieOptions);
        }
    }
}
