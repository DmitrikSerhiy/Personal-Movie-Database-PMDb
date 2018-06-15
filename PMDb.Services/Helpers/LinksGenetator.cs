using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class LinksGenetator<LRB, PP> : ILinksGenerator<LRB, PP> where LRB : LinkedResourceBase where PP : PaginationParameters
    {
        private IUrlHelper urlHelper;

        public LinksGenetator(IUrlHelper UrlHelper)
        {
            urlHelper = UrlHelper;
        }

        public List<LinkModel> CreateLinksForMovieList(LRB Resource, PP paginationParameters)
        {
            var movieList = (Resource as MovieListModel);
            var links = new List<LinkModel>
            {
                //let it be just in case
                //-----
                new LinkModel
                {
                    Href = UriProvider.ProvideForGetMovieList(
                     paginationParameters, UriType.NextPage, urlHelper, movieList),
                    Rel = "get_next_movieList",
                    Method = "GET"
                },

                new LinkModel
                {
                    Href = UriProvider.ProvideForGetMovieList(
                     paginationParameters, UriType.PreviousPage, urlHelper, movieList),
                    Rel = "get_previous_movieList",
                    Method = "GET"
                },
                //-----
                new LinkModel
                {
                    Href = UriProvider.ProvideForDeleteMovieList(urlHelper, movieList),
                    Rel = "delete_movieList",
                    Method = "DELETE"
                },

                new LinkModel
                {
                    Href = UriProvider.ProvideForUpdateMovieListName(
                    paginationParameters, urlHelper, movieList, "putHereNewName"),
                    Rel = "update_movieList_name",
                    Method = "PATCH"
                }
            };

            movieList.Movies.ForEach(movie =>
            {
                movie.Links.AddRange(CreateLinksForMovieInMovieList(movie));
            });
            return links;
        }

        public List<LinkModel> CreateLinksForMovieInMovieList(SimplifiedMovieModel movie)
        {
            var links = new List<LinkModel>
            {
                new LinkModel
                {
                    Href = UriProvider.ProvideForGetMovieInMovieList(urlHelper, movie.Title),
                    Rel = "get_movie",
                    Method = "GET"
                },

                new LinkModel
                {
                    Href = UriProvider.ProvideForAddMarkToMovieInMovieList(urlHelper, movie.Title, 0),//put here new mark
                    Rel = "add_mark",
                    Method = "PATCH"
                }
            };

            return links;
        }

        public List<LinkModel> CreateLinksForMovieListsPages(LRB Resource, PP paginationParameters)
        {
            var movieList = (Resource as MovieListModel);
            var links = new List<LinkModel>();
            for (int pageNumber = 1; pageNumber <= movieList.Movies.TotalPages; pageNumber++)
            {
                links.Add(new LinkModel
                {
                    Href = UriProvider
                        .ProvideLinksForPagesInMovieList(paginationParameters, movieList.Name, urlHelper, pageNumber),
                    Rel = "get_page" + pageNumber.ToString() + "_of_movieList",
                    Method = "GET"
                });
            }
            return links;
        }

        public List<LinkModel> CreateLinksForSearchedMovieListPages(LRB Resource, PP paginationParameters)
        {
            var movieList = (Resource as SearchedMovieListModel);
            var links = new List<LinkModel>();
            for (int pageNumber = 1; pageNumber <= movieList.Movies.TotalPages; pageNumber++)
            {
                links.Add(new LinkModel
                {
                    Href = UriProvider.ProvideLinksForPagesInSearchedMovieList(paginationParameters,
                        urlHelper, pageNumber, movieList.searchedMovie),
                    Rel = "get_page" + pageNumber.ToString() + "_of_searchedMovieList",
                    Method = "GET"
                });
            }

            return links;
        }
        public List<LinkModel> CreateLinksForSearchedMovieList()
        {
            return null;
        }

    }
}
