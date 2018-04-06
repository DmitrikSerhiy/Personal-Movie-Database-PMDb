using PMDb.Services.Helpers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.ServicesAbstraction
{
    public interface IMovieService
    {
        MovieModel GetMovie(int Id);
        MovieModel GetMovie(string title);
        IList<SimplifiedMovieModel> GetMovies(PaginationParameters getMoviesParameters);
        void MapToMovie(MovieModel movieModel);
        void AddMovie();
        void AddMark(double mark, string movieTitle);
        bool IsMarkValid();
        void DeleteMovie(string MovieName);
        int GetId();
        bool IsMovieExist(int movieId);
        bool IsMovieExist(string movieName);
        void UpdateMark(int movieId, double newMark);
        void DeleteMark(string movieName);
        string GeneratePreviousPageLink(bool hasPrevious, PaginationParameters getMoviesParameters);
        string GenerateNextPageLink(bool hasNext, PaginationParameters getMoviesParameters);

    }
}
