using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IMovieListRepository
    {
        MovieList CreateMovieList(string Name, bool IsDefault = false);
        MovieList GetMovieList(string Name);
        void DeleteMovieList(string movieListName);
        bool IsMovieListExist(int movieListId);
        bool IsMovieListExist(string movieListName);
        bool IsMovieExist(string movieName);
        bool IsMovieExistInList(string movieListName, string MovieName);
        MovieList AddMovieToList(string movieName, string movieListName);
        MovieList DeleteMovieFromList(string movieName, string movieListName);
        MovieList UpdateMovieListName(string oldName, string newName);
        void Save();
        List<string> GetDefaultListsName();
    }
}
