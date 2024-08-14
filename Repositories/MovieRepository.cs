using LettercubedApi.Data;
using LettercubedApi.DTOs;
using LettercubedApi.Models;
using LettercubedApi.utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Reflection;

namespace LettercubedApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMovieMapper _movieMapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<MovieRepository> _logger;
        public MovieRepository(ILogger<MovieRepository> logger, AppDbContext context,UserManager<AppUser> userManager, IMovieMapper mapper, IHttpContextAccessor accessor)
        {
            _context = context;
            _userManager = userManager;
            _movieMapper = mapper;
            _accessor = accessor;
            _logger = logger;
        }
        public async Task<MovieResponseDTO> CreateMovie(MovieDTO userData)
        {
            string userId = _userManager.GetUserId(_accessor.HttpContext.User);
            var Movie =  _movieMapper.DtoToMovie(userData,userId);
            await _context.AddAsync(Movie);
            await _context.SaveChangesAsync();
            return _movieMapper.MovieToResponseDTO(Movie);
            
        }


        public async Task<List<MovieResponseDTO>> GetAllMovies()
        {
            List<Movie> movies = await _context.Movies.ToListAsync();
            return _movieMapper.MovieToResponseDTO(movies);
        }

        public  async Task<MovieResponseDTO> GetMovieById(Guid id)
        {
            var Movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (Movie == null) return null; 
            return _movieMapper.MovieToResponseDTO(Movie);

        }

        public async Task<MovieResponseDTO> UpdateMovie(Guid id,MovieDTO userData)
        {
            var Movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (Movie == null) return null;
            
            string userId = _userManager.GetUserId(_accessor.HttpContext.User);
            Movie = _movieMapper.DtoToMovie(Movie.Id, userData, userId);
            _context.Movies.Update(Movie);
            await _context.SaveChangesAsync();

            return _movieMapper.MovieToResponseDTO(Movie);
        }

        public async Task<Guid> DeleteMovie(Guid Id) {
            var Movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == Id);
            if (Movie == null) return Guid.Empty;
            _context.Movies.Remove(Movie);
            await _context.SaveChangesAsync();
            return Id;  
        }

        public async Task<Guid> AddCategory(CategoryDTO userData)
        {
            Guid CategoryId = Guid.NewGuid();
            Category category = new Category()
            {
                Id = CategoryId,
                Name = userData.Name,
                Description = userData.Description
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CategoryId;
        }

        public async Task<Guid> AddMovieToCategory(Guid CategoryId, Guid MovieId)
        {
            
            await _context.Database.ExecuteSqlAsync($"INSERT INTO CategoryMovie (CategoriesId,MoviesId) VALUES ({CategoryId},{MovieId})");

            return CategoryId;


        }

        public async Task<CategoryResponseDTO> GetAllMoviesInCategory(Guid CategoryId)
        {
            var Category = await _context.Categories.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == CategoryId);

            if (Category == null) return null;

            List<MovieResponseDTO> moviesDtos = _movieMapper.MovieToResponseDTO(Category.Movies.ToList());
            CategoryResponseDTO res = new CategoryResponseDTO()
            {

                Id = Category.Id,
                Name = Category.Name,
                Description = Category.Description,
                Movies = moviesDtos

            };

            return res;
        }
    }
}
