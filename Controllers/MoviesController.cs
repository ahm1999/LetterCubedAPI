using LettercubedApi.DTOs;
using LettercubedApi.Repositories;
using LettercubedApi.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LettercubedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMovies() { 
            List<MovieResponseDTO> res = await _movieRepository.GetAllMovies();
            return Ok(res);
        }

        [Authorize(Roles =RolesConsts.EDITOR)]
        [HttpPost("AddMovie")]
        public async Task<IActionResult> CreateMovie(MovieDTO userData) {
            MovieResponseDTO res = await _movieRepository.CreateMovie(userData);
            return Ok(res);
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetMovieById(Guid Id) {
            MovieResponseDTO res = await _movieRepository.GetMovieById(Id);
            if (res == null) return BadRequest("No movie with that id exists");
            return Ok(res);
        }

        [Authorize(Roles = RolesConsts.EDITOR)]
        [HttpPatch("update/{Id:guid}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] Guid Id, [FromBody] MovieDTO userData) { 
            MovieResponseDTO res = await _movieRepository.UpdateMovie(Id, userData);
            if (res == null) return BadRequest("No movie with that id exists");
            return Ok(res); 
        }
        [Authorize(Roles = RolesConsts.EDITOR)]
        [HttpDelete("delete/{Id:guid}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] Guid Id) {
            Guid deletedId = await _movieRepository.DeleteMovie(Id);

            if (deletedId == Guid.Empty) return BadRequest("No movie with that id exists");

            return Ok($"Movie with the {deletedId} deleted");
        }


    }
}
