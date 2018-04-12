using PMDb.Domain.Interfaces;
using PMDb.Services.ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public class MovieListService : IMovieListService
    {
        private IMovieListRepository movieRepository;
        public MovieListService(IMovieListRepository MovieListRepository)
        {
            movieRepository = MovieListRepository;
        }

        public void AddMovieToList(int movieId, int movieListId)
        {
            throw new NotImplementedException();
        }

        public void CreateMovieList(string Name, bool IsDefault = false)
        {
            if (movieRepository.IsMovieListExist(Name) != true)
            {
                movieRepository.CreateMovieList(Name, IsDefault);
            }
            throw new NotImplementedException();
        }

        public void DeleteMovieFromList(int movieId, int movieListId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMovieList(int MovieListId)
        {
            throw new NotImplementedException();
        }

        public bool IsMovieListExist(int movieListId)
        {
            throw new NotImplementedException();
        }
    }
}
