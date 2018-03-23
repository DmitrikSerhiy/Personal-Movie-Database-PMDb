using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
        public IUrlHelper urlHelper;

        public MovieService(IMovieRepository MovieRepository, IUrlHelper UrlHelper)
        {
            movieRepository = MovieRepository;
            urlHelper = UrlHelper;
        }
        public MovieModel GetMovie(int id)
        {
            var movies = movieRepository.GetMovie(id);

            var movieModel = MovieMapper.Map(movies);

            return movieModel;
        }

        public IList<SimplifiedMovieModel> GetMovies(PaginationParameters getMoviesParameters)
        {
            var MovieCollectionBeforePaging = movieRepository.GetMovies();

            var movies = PagedList<Movie>.Create(MovieCollectionBeforePaging,
                getMoviesParameters.PageNumber,
                getMoviesParameters.PageSize);

            var PagedSimplifiedMovies = new PagedList<SimplifiedMovieModel>(
                movies.CurrentPage, movies.TotalPages, movies.PageSize,
                movies.TotalCount, movies.HasPrevious, movies.HasNext);

            foreach (var item in movies)
            {
                PagedSimplifiedMovies.Add(SimplifiedMovieMapper.Map(item));
            }
            return PagedSimplifiedMovies;
        }

        public string GenerateNextPageLink(bool nextPage, PaginationParameters getMoviesParameters)
        {
            return nextPage ?
               UriProvider.CreateMoviesUri(getMoviesParameters,
               UriType.NextPage, urlHelper as UrlHelper) : null;
        }

        public string GeneratePreviousPageLink(bool previousPage, PaginationParameters getMoviesParameters)
        {
            return previousPage ?
               UriProvider.CreateMoviesUri(getMoviesParameters,
               UriType.PreviousPage, urlHelper as UrlHelper) : null;
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
