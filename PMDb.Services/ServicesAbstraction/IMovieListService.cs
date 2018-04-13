using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.ServicesAbstraction
{
    public interface IMovieListService
    {
        MovieListModel CreateMovieList(string MovieListName, bool IsDefault = false);
        MovieListModel GetMovieList(string MovieListName);
        void DeleteMovieList(string MovieListName);
        bool IsMovieListExist(string movieListName);
        bool IsMovieExist(string movieName);
        void AddMovieToList(string movieName, string movieListName);
        void DeleteMovieFromList(string movieName, string movieListName);
    }
}
