using System.ComponentModel.DataAnnotations.Schema;

namespace LettercubedApi.Models
{
    public class MovieCategory
    {
        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Movie Movie { get; set; }

    }
}
