using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevMobile.ApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _GenreService;
        
        public GenresController(IGenreService GenreService)
        {
            _GenreService = GenreService;
        }

        // GET /Genres
        /// <summary>
        /// Retorna todos os Gêneros cadastrados
        /// </summary>
        /// <returns>Lista de gêneros</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            var GenresDto = await _GenreService.GetAll();
            return Ok(GenresDto);
        }

        // GET /Genres/{id}
        /// <summary>
        /// Busca um gênero por ID
        /// </summary>
        /// <param name="id">ID do gênero</param>
        /// <returns>Gênero encontrado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var Genre = await _GenreService.GetById(id);
            if (Genre == null) return NotFound();
            
            return Ok(Genre);
        }

        // POST /Genres
        /// <summary>
        /// Adiciona gênero a base de dados (Somente moderadores)
        /// </summary>
        /// <returns>Gênero criado no banco.</returns>
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task <ActionResult<GenreDto>> Create(CreateGenreDto newGenre)
        {
            var GenreDto = await _GenreService.Create(newGenre);

            return CreatedAtAction(
                nameof(GetById),
                new { id = GenreDto.Id },
                GenreDto
            );
        }

        // PUT /Genres/{id}
        /// <summary>
        /// Edita gênero na base de dados (Somente moderadores)
        /// </summary>
        [Authorize(Roles = "Moderator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateGenreDto updatedGenre)
        {
            var updated = await _GenreService.Update(id, updatedGenre);

            if (updated == false)
                return NotFound();

            return NoContent();
        }

        // DELETE /Genres/{id}
        /// <summary>
        /// Deleta gênero na base de dados (Somente moderadores)
        /// </summary>
        [Authorize(Roles = "Moderator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _GenreService.Delete(id);
            if (deleted == false)
                return NotFound();

            return NoContent();
        }
    }
}

    