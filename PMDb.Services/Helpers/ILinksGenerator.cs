using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Helpers
{
    public interface ILinksGenerator<T, U> where T : LinkedResourceBase where U : PaginationParameters
    {
        List<LinkModel> CreateLinksForMovieList(T Recource, U paginationParameters);
        List<LinkModel> CreateLinksForMovieListsPages(T Resource, U paginationParameters);
        List<LinkModel> CreateLinksForSearchedMovieListPages(T Resource, U paginationParameters);
    }
}
