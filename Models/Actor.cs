namespace LettercubedApi.Models
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string  Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
