using LettercubedApi.Data;
using LettercubedApi.DTOs;
using LettercubedApi.Models;
using LettercubedApi.utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LettercubedApi.Repositories
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMovieMapper _movieMapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<WatchListRepository> _logger;
        public WatchListRepository(ILogger<WatchListRepository> logger, AppDbContext context, UserManager<AppUser> userManager, IMovieMapper mapper, IHttpContextAccessor accessor)
        {
            _context = context;
            _userManager = userManager;
            _movieMapper = mapper;
            _accessor = accessor;
            _userManager = userManager;
            _logger = logger;
        }
        async Task<Guid> IWatchListRepository.AddToWatchList(Guid MovieId)
        {
            Guid watchListId;
            string userId =  _userManager.GetUserId(_accessor.HttpContext.User);

            WatchList fromDB_watchList1 = await _context.WatchLists.FirstOrDefaultAsync(wl => wl.UserId == userId);
            if (fromDB_watchList1 == null)
            {
                Guid wlID = Guid.NewGuid();
                WatchList new_watchList = new()
                {
                    Id = wlID,
                    UserId = userId
                };
                await _context.WatchLists.AddAsync(new_watchList);
                await _context.SaveChangesAsync();
                fromDB_watchList1 = new_watchList;
                watchListId = wlID;
            }            
            watchListId = fromDB_watchList1.Id;

            if (await _context.WatchListItems.AnyAsync(wli => wli.WatchListId == watchListId || wli.MovieId == MovieId)) return Guid.Empty;

            WatchListItem watchListItem = new() {
                IsWatched = false,
                MovieId = MovieId,
                WatchListId = watchListId
            };
            await _context.WatchListItems.AddAsync(watchListItem);
            await _context.SaveChangesAsync();
            return MovieId;
        }

        async Task<WatchListResponseDTO> IWatchListRepository.GetbyUserId(string UserId)
        {
           var _watchList = await  _context.WatchLists.Include(wl => wl.WatchListItems)
                                                      .ThenInclude(wlt => wlt.Movie)
                                                      .FirstOrDefaultAsync(wl => wl.UserId == UserId);
           if (_watchList == null) return null;

            var Response =  _watchList.WatchListItems.Select(wl => 
                                                                new WatchListMovie() {
                                                                        Id = wl.MovieId,
                                                                        Description = wl.Movie.Description,
                                                                        Title = wl.Movie.Title,
                                                                        CreatedIn = wl.Movie.CreatedIn,
                                                                        IsWatched = wl.IsWatched
                                                                    }
                                                            );


            var List= Response.ToList();
           WatchListResponseDTO responseDTO = new WatchListResponseDTO()
           {
               Id = _watchList.Id,
               UserId = _watchList.UserId,
               Movies = List
           };
            return responseDTO;
        }

        async Task<Guid> IWatchListRepository.RemoveFromWatchList(Guid MovieId)
        {
              string userId = _userManager.GetUserId(_accessor.HttpContext.User);
              var userWatchList = await _context.WatchLists.Include(wl=> wl.WatchListItems).FirstOrDefaultAsync(wl => wl.UserId == userId);
              if (userWatchList == null) return Guid.Empty;
              var toBeDeletedWLI = userWatchList.WatchListItems.FirstOrDefault(wli => wli.MovieId == MovieId);
              if (toBeDeletedWLI == null) return Guid.Empty;
              _context.WatchListItems.Remove(toBeDeletedWLI);
              await _context.SaveChangesAsync();
              return MovieId;  
        }

        async Task<Guid> IWatchListRepository.SetStatusToWatched(Guid MovieId)
        {
            string userId = _userManager.GetUserId(_accessor.HttpContext.User);
            var userWatchList = await _context.WatchLists.Include(wl => wl.WatchListItems).FirstOrDefaultAsync(wl => wl.UserId == userId);
            if (userWatchList == null) return Guid.Empty;
            var toBeUpdatedWLI = userWatchList.WatchListItems.FirstOrDefault(wli => wli.MovieId == MovieId);
            if (toBeUpdatedWLI == null) return Guid.Empty;
            toBeUpdatedWLI.IsWatched = true;
            _context.WatchListItems.Update(toBeUpdatedWLI);
            await _context.SaveChangesAsync();
            return MovieId;
        }
    }
}
