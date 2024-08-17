using LettercubedApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LettercubedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListRepository _watchListRepository;
        public WatchListController(IWatchListRepository watchListRepository)
        {
            _watchListRepository = watchListRepository;
        }


        [HttpGet("{UserId:guid}")]
        public async Task<IActionResult> GetWatchListById(Guid UserId)
        {
            var response = await _watchListRepository.GetbyUserId(UserId.ToString());
            return Ok(response);
        }
        [Authorize]
        [HttpPost("AddtoWatchList/{MovieId:guid}")]
        public async Task<IActionResult> AddToWatchList(Guid MovieId) {

            var response = await _watchListRepository.AddToWatchList(MovieId);
            if (response == Guid.Empty) return BadRequest("Movie Already Added");
            
            return Ok($"Movie {response.ToString()} added to Watch list");
        }
        [Authorize]
        [HttpDelete("RemoveFromWatchList/{MovieId:guid}")]
        public async Task<IActionResult> RemoveFromWatchList(Guid MovieId) {
            var response = await _watchListRepository.RemoveFromWatchList(MovieId);
            if (response == Guid.Empty) return BadRequest("Movie isn't on the list");
            return Ok($"Movie {response.ToString()} was removed from watch list");
        }
        [Authorize]
        [HttpPatch("SetToWatched/{MovieId:guid}")]
        public async Task<IActionResult> SetStatusToWatched(Guid MovieId) {
            var response = await _watchListRepository.SetStatusToWatched(MovieId);
            if(response == Guid.Empty) return BadRequest("Somthing wrong");

            return Ok($"Movie {response.ToString()} status updated to Watched");        
        }
        
        
        }
 }

