using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfeces
{
    public interface IRepository : IDisposable
    {
        IEnumerable<Movie> GetMovies();
        double? AddMark(int movieId);
        double? UpdateMark(int movieId);
        void DeleteMark(int movieId);
        double? GetMark(int movieId);
        void Save();
    }
}
