namespace LettercubedApi.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
