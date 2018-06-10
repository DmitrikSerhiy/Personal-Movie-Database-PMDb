using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PMDb.Services.Models;
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

        public static string ProvideURIForGetMovieList(PaginationParameters parameters,
            UriType type, IUrlHelper urlHelper, MovieListModel MovieList)
        {
            switch (type)
            {
                case UriType.NextPage : return MovieList.Movies.HasNext ?
                        (urlHelper as UrlHelper).Link("GetMovieList", new {
                            MovieList.Name,
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize
                        }) : null;
                case UriType.PreviousPage : return MovieList.Movies.HasPrevious ?
                        (urlHelper as UrlHelper).Link("GetMovieList", new {
                            MovieList.Name,
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize
                        }) : null;
                default: return null;
            }
        }

        public static string ProvideURIForDeleteMovieList(IUrlHelper urlHelper, 
            MovieListModel MovieList)
        {
            return !MovieList.IsDefault ?
                (urlHelper as UrlHelper).Link("DeleteMovieList", new {MovieList.Name,}) : null;
        }

        public static string ProvideURIForUpdateMovieListName(PaginationParameters parameters, 
            IUrlHelper urlHelper, MovieListModel MovieList, string NewName)
        {
            return (urlHelper as UrlHelper).Link("UpdateMovieListName", new
            {
                OldName = MovieList.Name,
                NewName = NewName,
                pageNumber = parameters.PageNumber,
                pageSize = parameters.PageSize
            });
        }

        public static string ProvideURIForGetMovieInMovieList(IUrlHelper urlHelper, string MovieName)
        {
            var uri = (urlHelper as UrlHelper).Link("GetMovie", new { title = "!/!@#$%^&*" });
            return FixSpaces(uri);
        }

        public static string ProvideURIForAddMarkToMovieInMovieList(IUrlHelper urlHelper, string MovieName, double mark)
        {
            var uri = (urlHelper as UrlHelper).Link("AddMark", new { title = FixSpaces(MovieName), mark });
            return FixSpaces(uri);
        }

        private static string FixSpaces (string uriParameters)
        {
            if(uriParameters.Contains("%20"))
            {
                uriParameters = uriParameters.Replace("%20", " ");
                return uriParameters;
            }
            return uriParameters;
        }

    }
}
