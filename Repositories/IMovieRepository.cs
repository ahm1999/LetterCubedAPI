﻿using LettercubedApi.DTOs;
using LettercubedApi.Models;

namespace LettercubedApi.Repositories
{
    public interface IMovieRepository
    {

        public Task<MovieResponseDTO> GetMovieById(Guid id);
        public Task<List<MovieResponseDTO>> GetAllMovies();

        public Task<MovieResponseDTO> CreateMovie(MovieDTO userData);

        public Task<MovieResponseDTO> UpdateMovie(Guid Id,MovieDTO userData);

        public Task<Guid> DeleteMovie(Guid id);

        public Task<Guid> AddCategory(CategoryDTO userData);

        public Task<Guid> AddMovieToCategory(Guid CategoryId, Guid MovieId);
        public Task<CategoryResponseDTO> GetAllMoviesInCategory(Guid CategoryId);
    


    }
}
