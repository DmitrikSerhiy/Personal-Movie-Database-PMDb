using PMDb.Domain.Core;
using System;
using System.Collections.Generic;

namespace PMDb.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Movie GetMovie(int movieId);
        IList<T> GetMovies();
        bool IsExist(int movieId);
    }
}
