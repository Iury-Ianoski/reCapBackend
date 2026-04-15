using DevMobile.ApiService.Dto.Book;
using DevMobile.ApiService.Dto.Genre;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevMobile.ApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET /books
        /// <summary>
        /// Retorna todos os livros cadastrados
        /// </summary>
        /// <returns>Lista de livros</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var booksDto = await _bookService.GetAll();
            return Ok(booksDto);
        }

        // GET /books/{id}
        /// <summary>
        /// Busca um livro por ID
        /// </summary>
        /// <param name="id">ID do livro</param>
        /// <returns>Livro encontrado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return NotFound();
            
            return Ok(book);
        }

        // POST /books
        /// <summary>
        /// Adiciona livro a base de dados (Somente moderadores)
        /// </summary>
        /// <returns>Livro criado no banco.</returns>
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task <ActionResult<BookDto>> Create(CreateBookDto newBook)
        {
            var bookDto = await _bookService.Create(newBook);

            return CreatedAtAction(
                nameof(GetById),
                new { id = bookDto.Id },
                bookDto
            );
        }

        // PUT /books/{id}
        /// <summary>
        /// Edita livro na base de dados (Somente moderadores)
        /// </summary>
        [Authorize(Roles = "Moderator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateBookDto updatedBook)
        {
            var updated = await _bookService.Update(id, updatedBook);

            if (updated == false)
                return NotFound();

            return NoContent();
        }

        // DELETE /books/{id}
        /// <summary>
        /// Deleta livro na base de dados (Somente moderadores)
        /// </summary>
        [Authorize(Roles = "Moderator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.Delete(id);
            if (deleted == false)
                return NotFound();

            return NoContent();
        }

        // POST /books/{bookId}/genres
        /// <summary>
        /// Adiciona um ou mais gêneros a um livro existente (Somente moderadores)
        /// </summary>
        /// <param name="bookId">ID do livro</param>
        /// <param name="dto">Lista de IDs de gêneros a serem associados ao livro</param>
        /// <returns>Sem conteúdo em caso de sucesso</returns>
        [Authorize(Roles = "Moderator")]
        [HttpPost("{bookId}/genres")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddGenres(int bookId, GenresListDto dto)
        {
            var result = await _bookService.AddGenres(bookId, dto.GenreIds);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

    