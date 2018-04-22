using Microsoft.AspNetCore.Mvc;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class LinksGenetator//<T> where T : LinkedResourceBase
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

        public void CreateLinksForMovieList(LinkedResourceBase Resource)
        {
            foreach (var movie in (Resource as MovieListModel).Movies)
            {
                movie.Links.Add(
                    new LinkModel(urlHelper.Link("GetMovie", new { title = movie.Title}),
                    "get_movie",
                    "GET"));

                movie.Links.Add(
                    new LinkModel(urlHelper.Link("AddMovieToList", 
                        new { MovieListName = "WatchLater", MovieTitle = movie.Title }),
                    "add_movie_to_watchLater",
                    "POST"));
                
                movie.Links.Add(
                    new LinkModel(urlHelper.Link("DeleteMark", new { title = movie.Title }),
                    "delete_movie",
                    "DELETE"));

                movie.Links.Add(
                    new LinkModel(urlHelper.Link("DeleteMark", new { title = movie.Title }),
                    "delete_movie",
                    "DELETE"));

                //let it be...


            }
            
        }
    }
}
