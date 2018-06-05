using PMDb.Services.Helpers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.ServicesAbstraction
{
    public interface IMovieListService
    {
        MovieListModel CreateMovieList(string MovieListName, PaginationParameters PaginationParameters, bool IsDefault = false);
        MovieListModel GetMovieList(string MovieListName, PaginationParameters PaginationParameters);
        void DeleteMovieList(string MovieListName);
        void DeleteDefaultMovieList(string MovieListName);
        bool IsMovieListExist(string movieListName);
        bool IsMovieExist(string movieName);
        bool IsMovieExistInList(string movieListName, string MovieName);
        MovieListModel AddMovieToList(string movieName, string movieListName, PaginationParameters PaginationParameters);
        MovieListModel DeleteMovieFromList(string movieName, string movieListName, PaginationParameters PaginationParameters);
        MovieListModel UpdateMovieListName(string oldName, string newName, PaginationParameters PaginationParameters);
    }
}
