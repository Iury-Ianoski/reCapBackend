using DevMobile.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevMobile.ApiService.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthenticationController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;

        }
        
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var token = await _tokenService.GenerateToken(loginDto);

            if (token == String.Empty) return Unauthorized();

            return Ok(token);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            await _userService.Register(registerDto);
            return Ok();
        }

    }
}
