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
        bool IsMovieExistInList(string movieListName, string MovieName);
        MovieListModel AddMovieToList(string movieName, string movieListName);
        MovieListModel DeleteMovieFromList(string movieName, string movieListName);
        MovieListModel UpdateMovieListName(string oldName, string newName);
    }
}
