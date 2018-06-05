using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class UriProvider
    {
        public static string CreateMoviesUri(PaginationParameters getMoviesParameters, 
                                            UriType type, 
                                            UrlHelper urlHelper)

        {
            switch (type)
            {
                case UriType.PreviousPage:
                    return urlHelper.Link("GetMovies", new
                         {
                             pageNumber = getMoviesParameters.PageNumber - 1,
                             pageSize = getMoviesParameters.PageSize
                         });
                    
                case UriType.NextPage:
                    return urlHelper.Link("GetMovies", new
                    {
                        pageNumber = getMoviesParameters.PageNumber + 1,
                        pageSize = getMoviesParameters.PageSize
                    });
                default: return null;
            }
        }

        public static string CreateGetMovieListUri(PaginationParameters getMoviesParameters,
            UriType type, UrlHelper urlHelper)
        {
            switch(type)
            {
                case UriType.NextPage : return urlHelper.Link("GetMovieList", new
                    {
                        pageNumber = getMoviesParameters.PageNumber + 1,
                        pageSize = getMoviesParameters.PageSize
                    });
                case UriType.PreviousPage : return urlHelper.Link("GetMovieList", new
                    {
                        pageNumber = getMoviesParameters.PageNumber - 1,
                        pageSize = getMoviesParameters.PageSize
                    });
                default: return null;
            }
        }
    }
}
