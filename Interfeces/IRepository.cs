using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfeces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetMovies();
        double? AddMark(double mark);
        double? UpdateMark(int movieId);
        void DeleteMark(int movieId);
        Movie GetMovie(int movieId);
        void Save();
    }
}
