namespace LettercubedApi.DTOs
{
    public class WatchListResponseDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<WatchListMovie> Movies { get; set; }
    }
}
