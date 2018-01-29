using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfeces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        //some extra methods for movie
    }
}
