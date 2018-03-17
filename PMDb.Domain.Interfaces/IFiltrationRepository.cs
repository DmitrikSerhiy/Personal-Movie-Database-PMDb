using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IFiltrationRepository : IRepository<Movie>
    {
        IList<Movie> Filter(MovieFilters movieFilters);
       
    }
}
