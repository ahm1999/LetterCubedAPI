using LettercubedApi.DTOs;
using LettercubedApi.Models;

namespace LettercubedApi.utils
{
    public  interface IMovieMapper
    {
        public  Movie DtoToMovie(MovieDTO dto,string UserId);
        public  Movie DtoToMovie(Guid MovieId ,MovieDTO dto,string UserId);
        public   MovieResponseDTO MovieToResponseDTO(Movie movie);
        public List<MovieResponseDTO> MovieToResponseDTO(List<Movie> movies);

       
    }
}
