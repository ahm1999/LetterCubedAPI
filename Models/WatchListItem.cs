using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LettercubedApi.Models
{
    [PrimaryKey(nameof(WatchListId),nameof(MovieId))]
    public class WatchListItem
    {
        [ForeignKey(nameof(WatchList))]
        public Guid WatchListId { get; set; }
        public WatchList WatchList { get; set; }

        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }

        public Movie Movie { get; set; }

        public bool IsWatched { get; set; }
    }
}
