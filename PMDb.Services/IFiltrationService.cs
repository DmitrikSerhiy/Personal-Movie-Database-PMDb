using PMDb.Domain.Core;
using PMDb.Services.Helpers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public interface IFiltrationService
    {
        void Filter(MovieFilters movieFilters);
        IList<SimplifiedMovieModel> GetFiltredMovies(PaginationParameters getMoviesParameters);
        string GeneratePreviousPageLink(bool hasPrevious, PaginationParameters getMoviesParameters);
        string GenerateNextPageLink(bool hasNext, PaginationParameters getMoviesParameters);
    }
}
