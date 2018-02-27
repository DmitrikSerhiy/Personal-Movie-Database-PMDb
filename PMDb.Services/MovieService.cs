using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Helpers;
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

        public PagedList<SimplifiedMovieModel> GetMovies(GetMoviesParameters getMoviesParameters)
        {
            var MovieCollectionBeforePaging = movieRepository.GetMovies();

            var movies = PagedList<Movie>.Create(MovieCollectionBeforePaging,
                getMoviesParameters.PageNumber,
                getMoviesParameters.PageSize);


            //var simplifiedMovie = PagedList<SimplifiedMovieModel>.Create()
            var movieModels = new List<SimplifiedMovieModel>();
            foreach (var item in movies)
            {
                movieModels.Add(SimplifiedMovieMapper.Map(item));
            }

            var PagedSimplifiedMovies = PagedList<SimplifiedMovieModel>.Create(
                movieModels.AsQueryable(),
                getMoviesParameters.PageNumber,
                getMoviesParameters.PageSize);

            return PagedSimplifiedMovies;
        }

        public bool IsMovieExist(int movieId)
        {
            return movieRepository.IsExist(movieId);
        }

        public bool IsMovieExist(string movieName)
        {
            return movieRepository.IsExist(movieName);
        }

        public void UpdateMark(int movieId, double newMark)
        {
            movieRepository.UpdateMark(movieId, newMark);
            movieRepository.Save();
        }

        public void DeleteMark(string movieName)
        {
            movieRepository.DeleteMark(movieName);
            movieRepository.Save();
        }
    }
}
