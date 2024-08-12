using LettercubedApi.DTOs;
using LettercubedApi.Repositories;
using LettercubedApi.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace LettercubedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpPost("create/{MovieId:guid}")]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromRoute] Guid MovieId, [FromBody] ReviewDTO userData) {

            var res =  await _reviewRepository.CreateReview(MovieId,userData);
            return Ok(res);
        }

        [HttpGet("/{ReviewId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid ReviewId) { 
            var res = await _reviewRepository.GetReviewById(ReviewId);
            if (res == null) return BadRequest("No Review with that Id");
            return Ok(res);
        }

        [HttpGet("Movie/{MovieId:guid}")]
        public async Task<IActionResult> GetAllMovieReviews([FromRoute]Guid MovieId ) {

            var res = await _reviewRepository.GetAllReviewsByMovieId(MovieId);
            if (res == null) return NotFound();
            return Ok(res);
            
        }

        [Authorize]
        [HttpDelete("delete/{ReviewId:guid}")]
        public async Task<IActionResult> DeleteReview([FromRoute]Guid ReviewId)
        {
            Guid res = await _reviewRepository.DeleteReviewById(ReviewId);
            if (res == Guid.Empty) return NotFound();
            return Ok($"Review with the Id {res.ToString()} Deleted");

        }

        [Authorize(Roles = RolesConsts.ADMIN)]
        [HttpDelete("delete/Admin/{ReviewId:guid}")]
        public async Task<IActionResult> DeleteReviewByIdAdmin([FromRoute] Guid ReviewId)
        {
            Guid res = await _reviewRepository.DeleteReviewByIdAdmin(ReviewId);
            if (res == Guid.Empty) return NotFound();
            return Ok($"Review with the Id {res.ToString()} Deleted");

        }


    }
}
