using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public interface IFiltrationService
    {
        SimplifiedMovieModel Filter(MovieFilters movieFilters);

    }
}
