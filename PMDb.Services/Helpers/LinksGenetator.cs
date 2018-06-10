using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class LinksGenetator <LRB, PP> : ILinksGenerator<LRB, PP> where LRB : LinkedResourceBase where PP : PaginationParameters
    {
        private IUrlHelper urlHelper;

        public LinksGenetator(IUrlHelper UrlHelper)
        {
            urlHelper = UrlHelper;
        }

        public List<LinkModel> CreateLinksForMovieList(LRB Resource, PP paginationParameters)
        {
            var links = new List<LinkModel>();
            var movieList = (Resource as MovieListModel);
            var paggedMovies = movieList.Movies;

            links.Add(new LinkModel{
                Href = UriProvider.ProvideURIForGetMovieList(
                     paginationParameters, UriType.NextPage, urlHelper, movieList),
                Rel = "get_next_movieList",
                Method = "GET"
            });

            links.Add(new LinkModel{
                Href = UriProvider.ProvideURIForGetMovieList(
                     paginationParameters, UriType.PreviousPage, urlHelper, movieList),
                Rel = "get_previous_movieList",
                Method = "GET"
            });

            links.Add(new LinkModel {
                Href = UriProvider.ProvideURIForDeleteMovieList(urlHelper, movieList),
                Rel = "delete_movieList",
                Method = "DELETE"});

            links.Add(new LinkModel{
                Href = UriProvider.ProvideURIForUpdateMovieListName(
                    paginationParameters, urlHelper, movieList, "putHereNewName"),
                Rel = "update_movieList_name",
                Method = "PATCH"
            });

            movieList.Movies.ForEach(movie =>
            {
                movie.Links.AddRange(CreateLinksForMovieInMovieList(movie));
            });
            


            return links;

        }

        public List<LinkModel> CreateLinksForMovieInMovieList(SimplifiedMovieModel movie)
        {
            var links = new List<LinkModel>();

            links.Add(new LinkModel {
                Href = UriProvider.ProvideURIForGetMovieInMovieList(urlHelper, movie.Title),
                Rel = "get_movie",
                Method = "GET"
            });

            links.Add(new LinkModel
            {
                Href = UriProvider.ProvideURIForAddMarkToMovieInMovieList(urlHelper, movie.Title, 0),//put here new mark
                Rel = "add_mark",
                Method = "PATCH"
            });

            return links;

            
        }
    }
}
