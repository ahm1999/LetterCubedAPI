namespace LettercubedApi.DTOs
{
    public class CategoryResponseDTO:CategoryDTO
    {
        public Guid Id { get; set; }

        public ICollection<MovieResponseDTO> Movies { get; set; }
       
    }
}
