using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IFiltrationRepository : IRepository<Movie>
    {
        void Filter(MovieFilters filters);
    }
}
