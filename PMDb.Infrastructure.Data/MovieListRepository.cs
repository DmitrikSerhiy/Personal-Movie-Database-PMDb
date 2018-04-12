using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class MovieListRepository : IMovieListRepository
    {
        private MovieContext context;
        public MovieListRepository(MovieContext Context)
        {
            context = Context;
        }

        public void AddMovieToList(int movieId, int movieListId)
        {
            var movieListMovie = context.MovieLists.SelectMany(ml => ml.MovieListMovies)
                .FirstOrDefault(ml => ml.MovieListId == movieListId);

            var movieList = context.MovieLists.FirstOrDefault(ml => ml.Id == movieListId);
            var movie = context.Movies.FirstOrDefault(m => m.Id == movieId);

            movieListMovie.Movie = movie;
            movieListMovie.MovieId = movieId;
            movieListMovie.MovieList = movieList;
            movieListMovie.MovieListId = movieListId;

        }

        public void CreateMovieList(string Name, bool IsDefault = false)
        {
            context.MovieLists.Add(new MovieList { IsDefault = IsDefault, Name = Name });
        }

        public void DeleteMovieList(int MovieListId)
        {
            var movieList = context.MovieLists.FirstOrDefault(ml => ml.Id == MovieListId);
            context.MovieLists.Remove(movieList);
        }

        public void DeleteMovieFromList(string movieName, int movieListId)
        {
            var movieList = context.MovieLists.FirstOrDefault(ml => ml.Id == movieListId);
            if(movieList.IsDefault != true)
            {
                var movieListMovie = movieList.MovieListMovies.FirstOrDefault(m => m.Movie.Title == movieName);
                movieListMovie.Movie = null;
                movieListMovie.MovieList = null;
            }
        }

        public bool IsMovieListExist(int movieListId)
        {
            return context.MovieLists.FirstOrDefault(ml => ml.Id == movieListId) == null ? false : true;
        }
        bool IsMovieListExist(string movieListName)
        {
            return context.MovieLists.FirstOrDefault(ml => ml.Name == movieListName) == null ? false : true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
