using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace DevMobile.ApiService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET /Reviews
        /// <summary>
        /// Retorna os últimas Reviews Publicadas baseado no limite.
        /// </summary>
        /// <param name="limit">limite de Reviews</param>
        /// <returns>Lista de reviews</returns>
        [HttpGet("latest/{limit}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetLatest(int limit)
        {
            var ReviewsDto = await _reviewService.GetLatest(limit);
            return Ok(ReviewsDto);
        }

        // GET /Reviews/bookId
        /// <summary>
        /// Retorna as reviews de um livro.
        /// </summary>
        /// <param name="bookId">Id do livro</param>
        /// <returns>Lista de reviews</returns>
        [HttpGet("bookId/{bookID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByBookId(int bookId)
        {
            var ReviewsDto = await _reviewService.GetReviewsByBookId(bookId);
            return Ok(ReviewsDto);
        }

        // GET /Reviews/userId
        /// <summary>
        /// Retorna as reviews de um usuário.
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <returns>Lista de reviews</returns>
        [HttpGet("userId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByUserId(int userId)
        {
            var ReviewsDto = await _reviewService.GetReviewsByUserId(userId);
            return Ok(ReviewsDto);
        }

        // GET /Reviews/{id}
        /// <summary>
        /// Busca review por ID
        /// </summary>
        /// <param name="id">ID da review</param>
        /// <returns>Review encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewDto>> GetById(int id)
        {
            var Review = await _reviewService.GetById(id);
            if (Review == null) return NotFound();
            
            return Ok(Review);
        }

        // POST /Reviews
        /// <summary>
        /// Adiciona review a base de dados
        /// </summary>
        /// <returns>Review criada no banco.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task <ActionResult<ReviewDto>> Create(CreateReviewDto newReview)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // ou, dependendo de como você montou o token:
            // var userId = User.FindFirst("sub")?.Value;

            var reviewDto = await _reviewService.Create(newReview, userId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = reviewDto.Id },
                reviewDto
            );
        }

        // PUT /Reviews/{id}
        /// <summary>
        /// Edita review na base de dados
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id, UpdateReviewDto updatedReview)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var review = await _reviewService.GetById(id);

            if(review.UserId != userId)
                return Unauthorized();

            var updated = await _reviewService.Update(id, updatedReview);

            if (updated == false)
                return NotFound();

            return NoContent();
        }

        // DELETE /Reviews/{id}
        /// <summary>
        /// Deleta review na base de dados
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var review = await _reviewService.GetById(id);

            if(review.UserId != userId)
                return Unauthorized();

            var deleted = await _reviewService.Delete(id);
            if (deleted == false)
                return NotFound();

            return NoContent();
        }
    }
}

    