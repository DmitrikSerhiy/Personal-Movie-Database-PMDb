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

        public IEnumerable<MovieModel> GetMovies()
        {
            var movies = movieRepository.GetMovies();
            List<MovieModel> movieModels = new List<MovieModel>();
            for (int i = 0; i < movies.Count(); i++)
            {
               // movieModels = MovieMapper.Map((movies[i]));
            }

            throw new NotImplementedException();
        }
    }
}
