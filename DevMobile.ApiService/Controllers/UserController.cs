using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Dto.User;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevMobile.ApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /users/search/{name}
        /// <summary>
        /// Retorna usuários que possuam parametro no nome
        /// </summary>
        /// <param name="name">Parte do nome do usuário</param>
        /// <returns>Lista de usuários</returns>
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDto>>> SearchUser(string name)
        {
            var usersDtos = await _userService.SearchByNamePart(name);
            if (usersDtos == null)return NotFound();

            return Ok(usersDtos);
        }

        // GET /users/{id}
        /// <summary>
        /// Busca um usuário por ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>usuário encontrado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();
            
            return Ok(user);
        }

        
    }
}

    