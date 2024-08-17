using LettercubedApi.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace LettercubedApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class WatchList
    {

        public Guid Id { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }

        public ICollection<WatchListItem> WatchListItems { get; set; }

    }
}
