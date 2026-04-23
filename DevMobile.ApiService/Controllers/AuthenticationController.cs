using DevMobile.ApiService.Dto.User;
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
        private readonly IAuthService _authService;

        public AuthenticationController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;

        }
        
        // POST /auth/login
        /// <summary>
        /// Realize login utilizando as credenciais de acesso de um usuário existente
        /// </summary>
        /// <returns>Token necessário para acessar demais APIs</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var token = await _tokenService.GenerateToken(loginDto);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Credenciais inválidas" });

            return Ok(new {BearerToken = token});
        }

        // POST /register
        /// <summary>
        /// Registre-se. Adiciona novo usuário à base de dados.
        /// </summary>
        /// <returns>Mensagem de sucesso</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            await _authService.Register(registerDto);
            return Created("", new { message = "Usuário criado com sucesso" });
        }

        // GET /me
        /// <summary>
        /// captura o usuário logado
        /// </summary>
        /// <returns>usuário</returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            var userDto = await _authService.GetUserFromToken(User);

            if (userDto == null)
                return Unauthorized();

            return Ok(userDto);
        }
    }
}
