using LettercubedApi.DTOs;
using LettercubedApi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LettercubedApi.utils
{
    public class MovieMapper:IMovieMapper
    {
        public Movie DtoToMovie(MovieDTO dto,string UserId) {
            var Movie = new Movie() { 
            Id = Guid.NewGuid(),
            Title = dto.Title,
            AddedOn = DateTime.UtcNow,
            CreatedIn = dto.CreatedIn,
            Description = dto.Description,
            AddedByUserId = UserId 
            };

            return Movie;
        }


        public MovieResponseDTO MovieToResponseDTO(Movie movie) {
            var Response = MapMovieToMovieRes(movie);
       
            return Response;
        }

        public List<MovieResponseDTO> MovieToResponseDTO(List<Movie> movies) {

            var Movies = movies.Select<Movie,MovieResponseDTO>((m) => {
                return MapMovieToMovieRes(m);
            });

            return Movies.ToList();
        }

        Movie IMovieMapper.DtoToMovie(Guid MovieId, MovieDTO dto, string UserId)
        {
            var Movie = new Movie()
            {
                Id = MovieId,
                Title = dto.Title,
                AddedOn = DateTime.UtcNow,
                CreatedIn = dto.CreatedIn,
                Description = dto.Description,
                AddedByUserId = UserId
            };

            return Movie;
        }

    

    private MovieResponseDTO MapMovieToMovieRes(Movie movie) {
            var res = new MovieResponseDTO() {
                Id = movie.Id,
                CreatedIn = movie.CreatedIn,
                Description = movie.Description,
                Title = movie.Title
            };

            return res;  
        }


    }
}
