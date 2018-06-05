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

        public void CreateLinksForSearchMovie(LinkedResourceBase Resource, string MovieName)
        {
            Resource.Links.Add(new LinkModel(
                urlHelper.Link("AddMovieToList",
                new { MovieListName = "WatchLater", MovieTitle = MovieName }),
                "add_movie_to_watchLater",
                "POST"));

            //Add Mark
            //Add to any other list
        }

        public void CreateLinksForGetMovie(LinkedResourceBase Resource, string MovieName)
        {
            Resource.Links.Add(new LinkModel(
                urlHelper.Link("GetMovie", new { title = MovieName }),
                "self",
                "GET"));

            Resource.Links.Add(new LinkModel(
                urlHelper.Link("AddMovieToList", 
                new { MovieListName = "WatchLater", MovieTitle = MovieName }),
                "add_movie_to_watchLater",
                "POST"));

            //resource.Links.Add(new LinkModel(
            //    urlHelper.Link("AddMovie", new { movieModel = movie }),
            //    "add_to_library",
            //    "POST"));
        }

        public List<LinkModel> CreateLinksForMovieList(LRB Resource, PP paginationParameters)
        {
            var links = new List<LinkModel>();
            var movieList = (Resource as MovieListModel);
            var paggedMovies = movieList.Movies;

            //PP paginationLinks = new PaginationParameters()
            //{
            //    PageNumber
            //}
            //var paginationMetada = new  
            //{
            //    totalCount = paggedMovies.TotalCount,
            //    pageSize = paggedMovies.PageSize,
            //    currentPage = paggedMovies.CurrentPage,
            //    totalPages = paggedMovies.TotalPages,
            //    previousPageLink = GeneratePreviousPageLink(paggedMovies.HasPrevious, paginationParameters),
            //    nextPageLink = GenerateNextPageLink(paggedMovies.HasNext, paginationParameters)
            //};



            links.Add(new LinkModel());
            //    new LinkModel(urlHelper.Link(
            //        "GetMovieList", 
            //        new {Name = movieList.Name,
            //            paginationParameters = GenerateNextPageLink(paggedMovies.HasNext, paginationParameters)}),
            //    "get_next_movieList",
            //    "GET"));

            //links.Add(
            //new LinkModel(urlHelper.Link(
            //    "GetMovieList",
            //    new {Name = movieList.Name,
            //        paginationParameters = GenerateNextPageLink(paggedMovies.HasPrevious, paginationParameters)}),
            //"get_previous_movieList",
            //"GET"));

            return links;

            //foreach (var movie in (Resource as MovieListModel).Movies)
            //{
            //    movie.Links.Add(
            //        new LinkModel(urlHelper.Link("GetMovie", new { title = movie.Title}),
            //        "get_movie",
            //        "GET"));

            //    movie.Links.Add(
            //        new LinkModel(urlHelper.Link("AddMovieToList", 
            //            new { MovieListName = "WatchLater", MovieTitle = movie.Title }),
            //        "add_movie_to_watchLater",
            //        "POST"));

            //    movie.Links.Add(
            //        new LinkModel(urlHelper.Link("DeleteMark", new { title = movie.Title }),
            //        "delete_movie",
            //        "DELETE"));

            //    movie.Links.Add(
            //        new LinkModel(urlHelper.Link("DeleteMark", new { title = movie.Title }),
            //        "delete_movie",
            //        "DELETE"));

            //let it be...


        }

        public string GenerateNextPageLink(bool nextPage, PaginationParameters getMoviesParameters)
        {
            return nextPage ?
               UriProvider.CreateGetMovieListUri(getMoviesParameters,
               UriType.NextPage, urlHelper as UrlHelper) : null;
        }

        public string GeneratePreviousPageLink(bool previousPage, PaginationParameters getMoviesParameters)
        {
            return previousPage ?
               UriProvider.CreateGetMovieListUri(getMoviesParameters,
               UriType.PreviousPage, urlHelper as UrlHelper) : null;
        }
    }
}
