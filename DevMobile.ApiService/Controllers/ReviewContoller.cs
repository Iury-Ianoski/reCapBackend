using DevMobile.ApiService.Dto.Review;
using DevMobile.ApiService.Entities;
using DevMobile.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// Adiciona review a base de dados (Somente moderadores)
        /// </summary>
        /// <returns>Review criada no banco.</returns>
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task <ActionResult<ReviewDto>> Create(CreateReviewDto newReview)
        {
            var ReviewDto = await _reviewService.Create(newReview);

            return CreatedAtAction(
                nameof(GetById),
                new { id = ReviewDto.Id },
                ReviewDto
            );
        }

        // PUT /Reviews/{id}
        /// <summary>
        /// Edita review na base de dados (Somente moderadores)
        /// </summary>
        [Authorize(Roles = "Moderator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateReviewDto updatedReview)
        {
            var updated = await _reviewService.Update(id, updatedReview);

            if (updated == false)
                return NotFound();

            return NoContent();
        }

        // DELETE /Reviews/{id}
        /// <summary>
        /// Deleta review na base de dados (Somente moderadores)
        /// </summary>
        [Authorize(Roles = "Moderator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewService.Delete(id);
            if (deleted == false)
                return NotFound();

            return NoContent();
        }
    }
}

    