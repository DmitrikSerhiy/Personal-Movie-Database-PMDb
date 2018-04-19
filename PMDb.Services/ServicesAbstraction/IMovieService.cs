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
        bool IsMarkValid(double movieModel);
        bool IsMarkValid(MovieModel movieModel);
        bool IsReviewValid(MovieModel movieModel);
        void EditReview(string movieName, string review);
        void DeleteReview(string movieName);
        void AddTags(TagParameters tagParameters, string movieName);
        void DeleteTag(string tagName, string movieName);
        int GetId();
        string GetName();
        bool IsMovieExist(int movieId);
        bool IsMovieExist(string movieName);
        void UpdateMark(string movieName, double newMark);
        void DeleteMark(string movieName);
        string GeneratePreviousPageLink(bool hasPrevious, PaginationParameters getMoviesParameters);
        string GenerateNextPageLink(bool hasNext, PaginationParameters getMoviesParameters);

    }
}
