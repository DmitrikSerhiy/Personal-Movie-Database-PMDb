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

        public void AddMovieToList(string movieName, string movieListName)
        {
            var movieListMovie = context.MovieLists.SelectMany(ml => ml.MovieListMovies)
                .FirstOrDefault(ml => ml.MovieList.Name == movieListName);

            var movieList = context.MovieLists.FirstOrDefault(ml => ml.Name == movieListName);
            var movie = context.Movies.FirstOrDefault(m => m.Title == movieName);

            movieListMovie.Movie = movie;
            movieListMovie.MovieId = movie.Id;
            movieListMovie.MovieList = movieList;
            movieListMovie.MovieListId = movieList.Id;

        }

        public MovieList CreateMovieList(string Name, bool IsDefault = false)
        {
            var movieList = new MovieList { IsDefault = IsDefault, Name = Name };
            context.MovieLists.Add(movieList);
            return movieList;
        }

        public void DeleteMovieList(string MovieListName)
        {
            var movieList = context.MovieLists.FirstOrDefault(ml => ml.Name == MovieListName);
            context.MovieLists.Remove(movieList);
        }

        public void DeleteMovieFromList(string movieName, string movieListName)
        {
            var movieList = context.MovieLists.FirstOrDefault(ml => ml.Name == movieListName);
            if (movieList.IsDefault != true)
            {
                var movieListMovie = movieList.MovieListMovies.FirstOrDefault(m => m.Movie.Title == movieName);

                context.Movies.FirstOrDefault(m => m.Title == movieName).MovieListMovie = null;
                movieListMovie.Movie = null;
                //movieListMovie = null;
                //movieListMovie.MovieList = null;
                //movieListMovie.MovieId = null;
            }
        }

        public MovieList GetMovieList(string Name)
        {
            return context.MovieLists.FirstOrDefault(ml => ml.Name == Name);
        }


        public bool IsMovieListExist(int movieListId)
        {
            return context.MovieLists.FirstOrDefault(ml => ml.Id == movieListId) == null ? false : true;
        }
        public bool IsMovieListExist(string movieListName)
        {
            return context.MovieLists.FirstOrDefault(ml => ml.Name == movieListName) == null ? false : true;
        }

        public bool IsMovieExist(string movieName)
        {
            return context.Movies.FirstOrDefault(m => m.Title == movieName) == null ? false : true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
