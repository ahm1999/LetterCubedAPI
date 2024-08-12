using LettercubedApi.DTOs;
using LettercubedApi.Models;

namespace LettercubedApi.utils
{
    public class ReviewMapper : IReviewMapper
    {
        public Reviews DtoToReview(ReviewDTO dto,string UserId ,Guid MovieId)
        {
            Reviews res = new Reviews() { 
            Id = Guid.NewGuid(),
            Content = dto.Content,
            CreatedOn = DateTime.UtcNow,
            Rating = dto.Rating,
            Title = dto.Title,
            MovieId = MovieId,
            AppUserId = UserId
            
            };

            return res;
        }

        public ReviewResponseDTO ReviewToDTO(Reviews review)
        {
            var res = new ReviewResponseDTO()
            {
                ReviewId = review.Id,   
                Content = review.Content,
                MovieId = review.MovieId,
                MovieName  =review.Movie.Title,
                Title = review.Title,
                Rating=review.Rating,
                UserId = review.AppUserId,
                UserName = review.User.UserName
                
            };
            return res; 
        }

        public List<ReviewResponseDTO> ReviewToDTO(List<Reviews> dto)
        {
          var list =  dto.Select(d => {
                                return ReviewToDTO(d);
                            });
            return list.ToList();

        }
    }
}
