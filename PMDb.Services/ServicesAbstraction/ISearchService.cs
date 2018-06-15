using PMDb.Services.Helpers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.ServicesAbstraction
{
   public interface ISearchService
    {
        bool IsExist();
        void SerializeMovie(string MovieString);
        void SerializeMovieList(string MovieString);
        void MapToMovieModel();
        int CalculatePages(int amount);
        
        MovieModel GetMovie();
        List<DowloadedMovieInMovieListModel> GetMovies();
        void ValidateMovie();
        List<DowloadedMovieInMovieListModel> ValidateMovieList();
        SearchedMovieListModel CreatePagedLinkedMovieList(
            List<DowloadedMovieInMovieListModel> composedMovies, string movieTitle,
            PaginationParameters paginationParameters);
        int GetMovieAmount();
    }
}
