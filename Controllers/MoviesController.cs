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


        [HttpGet("Category/{CategoryId:guid}")]
        public async Task<IActionResult> GetMoviesInCategory([FromRoute] Guid CategoryId) {
            var res = await _movieRepository.GetAllMoviesInCategory(CategoryId);
            if (res == null) return BadRequest("Category doesn't exist");
            return Ok(res);

        }



        [Authorize(Roles = RolesConsts.EDITOR)]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO userData) {
            Guid CategoryId = await _movieRepository.AddCategory(userData);
            return Ok($"A new Category created with the Id : {CategoryId}");
            
        }

        [Authorize(Roles = RolesConsts.EDITOR)]
        [HttpPost("AddTOCategory/{CategoryId:guid}/{MovieId:guid}")]
        public async Task<IActionResult> AddToCategory([FromRoute] Guid CategoryId, [FromRoute] Guid MovieId) {
            Guid Id =  await _movieRepository.AddMovieToCategory(CategoryId, MovieId);
            if (Id == Guid.Empty) return BadRequest("Movie Id Or Category Id is wrong");
            return Ok("Movie added to Category");

        }



    }
}
