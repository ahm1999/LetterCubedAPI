using LettercubedApi.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace LettercubedApi.Models
{
    public class Reviews
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Rating { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }

        public AppUser User { get; set; }

        public Guid MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
