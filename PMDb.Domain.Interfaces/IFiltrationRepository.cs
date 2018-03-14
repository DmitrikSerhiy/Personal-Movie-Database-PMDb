using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IFiltrationRepository : IRepository<Movie>
    {
        Movie Filter(MovieFilters movieFilters);
       
    }
}
