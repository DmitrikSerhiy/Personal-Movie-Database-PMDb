using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.ServicesAbstraction
{
    public interface IMovieListService
    {
        void CreateMovieList(string Name, bool IsDefault = false);
        void DeleteMovieList(int MovieListId);
        bool IsMovieListExist(int movieListId);
        void AddMovieToList(int movieId, int movieListId);
        void DeleteMovieFromList(int movieId, int movieListId);
    }
}
