﻿using PMDb.Domain.Interfaces;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using PMDb.Services.ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public class MovieListService : IMovieListService
    {
        private IMovieListRepository movieListRepository;

        public MovieListService(IMovieListRepository MovieListRepository)
        {
            movieListRepository = MovieListRepository;
        }

        public void AddMovieToList(string movieName, string movieListName)
        {
            movieListRepository.AddMovieToList(movieName, movieListName);
            movieListRepository.Save();
        }

        public MovieListModel CreateMovieList(string Name, bool IsDefault = false)
        {
            if (IsDefault != true)
            {
                var movieList = movieListRepository.CreateMovieList(Name);
                movieListRepository.Save();
                return MovieListMapper.Map(movieList);
            }
            var defaultMovieList = movieListRepository.CreateMovieList(Name, true);//create default movieList
            movieListRepository.Save();
            return MovieListMapper.Map(defaultMovieList);
        }

        public void DeleteMovieFromList(string movieName, string movieListName)
        {
            movieListRepository.DeleteMovieFromList(movieName, movieListName);
            movieListRepository.Save();
        }

        public void DeleteMovieList(string MovieListName)
        {
            if (movieListRepository.GetMovieList(MovieListName).IsDefault != true)
            {
                movieListRepository.DeleteMovieList(MovieListName);
                movieListRepository.Save();
            }
        }

        public MovieListModel GetMovieList(string MovieListName)
        {
            var movieList = movieListRepository.GetMovieList(MovieListName);
            var mappedMovieList = MovieListMapper.Map(movieList);
            return mappedMovieList;
        }

        public bool IsMovieListExist(string movieListName)
        {
            return movieListRepository.IsMovieListExist(movieListName);
        }

        public bool IsMovieExist(string movieName)
        {
            return movieListRepository.IsMovieExist(movieName);
        }
    }
}
