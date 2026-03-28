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

        // POST /auth/register
        /// <summary>
        /// Registre-se. Adiciona novo usuário à base de dados.
        /// </summary>
        /// <returns>Mensagem de sucesso</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            await _userService.Register(registerDto);
            return Created("", new { message = "Usuário criado com sucesso" });
        }

    }
}
