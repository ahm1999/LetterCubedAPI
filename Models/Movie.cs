using LettercubedApi.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace LettercubedApi.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateOnly CreatedIn { get; set; }

        public DateTime AddedOn { get; set; }

        [ForeignKey("AppUser")]
        public string AddedByUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
