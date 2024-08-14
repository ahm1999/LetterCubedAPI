using System.ComponentModel.DataAnnotations;

namespace LettercubedApi.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
