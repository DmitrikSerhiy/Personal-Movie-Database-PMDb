using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie GetMovie(int movieId);
        Movie GetMovie(string title);
        void AddMovie(Movie movie);
        void AddMark(double mark, string movieTitle);
        int GetId(Movie movie);
        void UpdateMark(int movieId, double newMark);
        void Save();
        void DeleteMark(string movieName);
        void DeleteMovie(string MovieName);
    }
}
