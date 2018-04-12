using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IMovieListRepository
    {
        void CreateMovieList(string Name, bool IsDefault = false);
        void DeleteMovieList(int MovieListId);
        bool IsMovieListExist(int movieListId);
        bool IsMovieListExist(string movieListName);
        void AddMovieToList(string movieName, int movieListId);
        void DeleteMovieFromList(string movieName, int movieListId);
        void Save();
    }
}
