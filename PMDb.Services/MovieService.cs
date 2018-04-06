using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Helpers;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using PMDb.Services.ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDb.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository movieRepository;
        private MovieServiceValidator validator;
        private MovieModel MovieModel;
        public Movie movieToAdd;
        public double markToAdd;
        public IUrlHelper urlHelper;

        public MovieService(IMovieRepository MovieRepository, IUrlHelper UrlHelper)
        {
            movieRepository = MovieRepository;
            urlHelper = UrlHelper;
            validator = new MovieServiceValidator();
        }

        public void MapToModel(Movie movie)
        {
            MovieModel = MovieMapper.Map(movie);
        }
        public MovieModel GetMovie(int id)
        {
            var movie = movieRepository.GetMovie(id);
            MapToModel(movie);
            return MovieModel;
        }

        public MovieModel GetMovie(string title)
        {
            var movie = movieRepository.GetMovie(title);
            MapToModel(movie);
            return MovieModel;
        }

        public void MapToMovie(MovieModel movieModel)
        {
            movieToAdd = MovieMapper.Map(movieModel);
        }
        public void AddMovie()
        {
            movieRepository.AddMovie(movieToAdd);
            movieRepository.Save();
        }

        public void DeleteMovie(string MovieName)
        {
            movieRepository.DeleteMovie(MovieName);
            movieRepository.Save();
        }

        public int GetId()
        {
            return movieToAdd.Id;
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

        public void AddMark(double mark, string movieTitle)
        {
            markToAdd = mark;
            movieRepository.AddMark(mark, movieTitle);
            movieRepository.Save();
            
        }

        public bool IsMarkValid()
        {
            return validator.Validate(this).IsValid;//that's bool shit cannot add a ruleset
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
