namespace LettercubedApi.DTOs
{
    public class MovieResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateOnly CreatedIn { get; set; }
    }
}
