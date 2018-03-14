using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie GetMovie(int movieId);
        void AddMark(double mark);
        void UpdateMark(int movieId, double newMark);
        void Save();
        void DeleteMark(string movieName);
    }
}
