using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PMDb.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetMovies();
        bool IsExist(int movieId);
        bool IsExist(string movieName);
    }
}
