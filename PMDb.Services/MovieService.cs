using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDb.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository movieRepository;
        public MovieService(IMovieRepository MovieRepository)
        {
            movieRepository = MovieRepository;
        }
        public MovieModel GetMovie(int id)
        {
            var movies = movieRepository.GetMovie(id);

            var movieModel = MovieMapper.Map(movies);

            return movieModel;
        }

        public IList<MovieModel> GetMovies()
        {
            var movies = movieRepository.GetMovies() as List<Movie>;
            List<MovieModel> movieModels = new List<MovieModel>();
            foreach (var item in movies)
            {
                movieModels.Add(MovieMapper.Map(item));
            }

            return movieModels;
        }

        public bool IsMovieExist(int movieId)
        {
            return movieRepository.IsExist(movieId);
        }

        public void UpdateMark(int movieId, double newMark)
        {
            movieRepository.UpdateMark(movieId, newMark);
            movieRepository.Save();
        }
    }
}
