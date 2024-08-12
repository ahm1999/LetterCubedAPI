using LettercubedApi.Data;
using LettercubedApi.DTOs;
using LettercubedApi.Models;
using LettercubedApi.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LettercubedApi.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReviewMapper _ReviewMapper;
        private readonly IHttpContextAccessor _accessor;
        public ReviewRepository(AppDbContext context, UserManager<AppUser> userManager, IReviewMapper ReviewMapper, IHttpContextAccessor accessor)
        {
            _context = context;
            _userManager = userManager;
            _ReviewMapper = ReviewMapper;
            _accessor = accessor;
        }
        public async Task<Guid> CreateReview(Guid MovieId, ReviewDTO userData)
        {
            
            Reviews review = _ReviewMapper.DtoToReview(userData,_userManager.GetUserId(_accessor.HttpContext.User),MovieId);

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            
            var res  = review.Id;

            return res;
        }

        public async Task<Guid> DeleteReviewById(Guid ReviewId)
        {
            var review = await  _context.Reviews.FirstOrDefaultAsync(r => r.Id == ReviewId);
            if (review == null )return Guid.Empty;
            if (review.AppUserId != _userManager.GetUserId(_accessor.HttpContext.User)) return Guid.Empty ;

            _context.Reviews.Remove(review);
            var reviewID = review.Id;
            await _context.SaveChangesAsync();

            return reviewID;


        }

        public async Task<Guid> DeleteReviewByIdAdmin(Guid ReviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == ReviewId);
            if (review == null) return Guid.Empty;
            _context.Reviews.Remove(review);
            var reviewID = review.Id;
            await _context.SaveChangesAsync();

            return reviewID;
        }

        public async Task<List<ReviewResponseDTO>> GetAllReviewsByMovieId(Guid MovieId)
        {
            if (!await _context.Movies.AnyAsync(m => m.Id == MovieId)) return null;
          

            List<Reviews>reviews = await _context.Reviews.Where(r => r.MovieId == MovieId).Include(r => r.User).Include(r => r.Movie).ToListAsync();

            return _ReviewMapper.ReviewToDTO(reviews);
        }

        public async Task<ReviewResponseDTO> GetReviewById(Guid ReviewId)
        {
            Reviews review = await _context.Reviews.Include(r => r.User).Include(r =>r.Movie).FirstOrDefaultAsync(r=> r.Id ==ReviewId);
            if (review == null) return null;

            return _ReviewMapper.ReviewToDTO(review);

        }
    }
}
