using LettercubedApi.DTOs;
using System.Security.Permissions;

namespace LettercubedApi.Repositories
{
    public interface IWatchListRepository
    {
        public Task<WatchListResponseDTO> GetbyUserId(string UserId);
        public Task<Guid> AddToWatchList(Guid MovieId);

        public Task<Guid> RemoveFromWatchList(Guid MovieId);

        public Task<Guid> SetStatusToWatched(Guid MovieId);
    }
}
