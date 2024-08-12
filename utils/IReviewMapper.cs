using LettercubedApi.DTOs;
using LettercubedApi.Models;

namespace LettercubedApi.utils
{
    public interface IReviewMapper
    {
        public Reviews DtoToReview(ReviewDTO dto,string UserId , Guid MovieId); 
        public  ReviewResponseDTO ReviewToDTO(Reviews review); 
        public  List<ReviewResponseDTO> ReviewToDTO(List<Reviews> review); 
    }
}
