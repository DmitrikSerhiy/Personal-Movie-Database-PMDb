﻿using Microsoft.EntityFrameworkCore;
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


        public MovieList AddMovieToList(string movieListName, string movieName)
        {
            var movie = context.Movies
                .Include(m => m.MovieListMovie).ThenInclude(mlm => mlm.MovieList)
                .FirstOrDefault(m => m.Title == movieName);

            var movieList = context.MovieLists
                .Include(ml => ml.MovieListMovies)
                .ThenInclude(m => m.Movie)
                .FirstOrDefault(ml => ml.Name == movieListName);

            movieList.MovieListMovies.Add(new MovieListMovie {Movie = movie});

            return movieList;

        }

        public int GetMovieListLenght(string movieListName)
        {
            return context.MovieLists.Where(ml => ml.Name == movieListName)
                .SelectMany(m => m.MovieListMovies).Count();
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

        public MovieList DeleteMovieFromList(string movieName, string movieListName)
        {
            var movieList = context.MovieLists
                .Include(ml => ml.MovieListMovies)
                .ThenInclude(m => m.Movie)
                .FirstOrDefault(ml => ml.Name == movieListName);

            if (movieList.IsDefault != true)
            {
                var movieListMovie = movieList.MovieListMovies.FirstOrDefault(m => m.Movie.Title == movieName);
                movieList.MovieListMovies.Remove(movieListMovie);
            }
            return movieList;
        }

        public MovieList GetMovieList(string Name)
        {
            return context.MovieLists
                .Include(ml => ml.MovieListMovies)
                    .ThenInclude(m => m.Movie)
                        .ThenInclude(r => r.Rating)

                .Include(ml => ml.MovieListMovies)
                    .ThenInclude(m => m.Movie)
                        .ThenInclude(r => r.MovieListMovie)
                            .ThenInclude(mll => mll.MovieList)

                .Include(ml => ml.MovieListMovies)
                    .ThenInclude(m => m.Movie)
                        .ThenInclude(mt => mt.MovieTag)
                            .ThenInclude(t => t.Tag)
                .FirstOrDefault(ml => ml.Name == Name);
        }

        public List<MovieListMovie> GetMovieListMovieForMovie(string movieTitle)
        {
            return context.Movies
                .Where(ml => ml.Title == movieTitle)
                .SelectMany(mlm => mlm.MovieListMovie).ToList();

              
        }


        public MovieList UpdateMovieListName(string oldName, string newName)
        {
            var movieList = context.MovieLists.FirstOrDefault(ml => ml.Name == oldName);
            context.Entry(movieList).Property(ml => ml.Name).CurrentValue = newName;
            return movieList;
        }

        public bool IsMovieExistInList(string movieListName, string movieName)
        {
            var movieList = context.MovieLists
                .Include(ml => ml.MovieListMovies).ThenInclude(m => m.Movie)
                .Where(ml => ml.Name == movieListName);

            return movieList.SelectMany(mlm => mlm.MovieListMovies)
                .Where(ml => ml.Movie.Title == movieName).Count() == 0 ? false : true;
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

        public List<string> GetDefaultListsName()
        {
            List<string> listsNames = new List<string>();
            listsNames.AddRange(context.MovieLists.Where(m => m.IsDefault).Select(ml => ml.Name));
            return listsNames;
        } 
    }
}
