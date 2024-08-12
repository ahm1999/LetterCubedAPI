using LettercubedApi.DTOs;

namespace LettercubedApi.Repositories
{
    public interface IReviewRepository
    {
        public Task<Guid> CreateReview(Guid MovieId,ReviewDTO userData);
        public Task<ReviewResponseDTO> GetReviewById(Guid ReviewId);
        public Task<Guid> DeleteReviewById(Guid ReviewId);
        public Task<Guid> DeleteReviewByIdAdmin(Guid ReviewId);
        public Task<List<ReviewResponseDTO>> GetAllReviewsByMovieId(Guid MovieId);
    }
}
