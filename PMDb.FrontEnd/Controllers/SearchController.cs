using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMDb.Services;
using PMDb.Services.Helpers;
using PMDb.Services.Models;
using PMDb.Services.ServicesAbstraction;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private ISearchService searchService;
        private string key = "f6a55b6a";

        public SearchController(ISearchService SearchService)
        {
            searchService = SearchService;
        }

        [HttpGet("{title}")]
        public IActionResult FindMovie(string title)
        {
            var uriString = $"http://www.omdbapi.com/?apikey={key}&t={title}";

            using (var wc = new WebClient())
            {
                var json_data = string.Empty;

                json_data = wc.DownloadString(uriString);
                searchService.SerializeMovie(json_data);
                searchService.ValidateMovie();
                if (!searchService.IsExist()){
                    return NotFound();
                }

                searchService.MapToMovieModel();
                //searchService.AddLinks();
                var movie = searchService.GetMovie();

                return Ok(movie);
            }
        }

        [HttpGet("{movieTitle}", Name = "SearchMovies")]
        public IActionResult SearchMovies(string movieTitle, PaginationParameters paginationParameters)
        {
            var firstRequesturiString = $"http://www.omdbapi.com/?apikey={key}&s={movieTitle}&page=1";
            Uri targetUri = new Uri(firstRequesturiString);

            using (var wc = new WebClient())
            {
                var firstRequest = string.Empty;

                firstRequest = wc.DownloadString(firstRequesturiString);
                searchService.SerializeMovieList(firstRequest);
                var amount = searchService.GetMovieAmount();
                if (amount == 0) return NotFound();
                var pages = searchService.CalculatePages(amount);
                if (pages > 10) pages = 10;

                var uriForPage = $"http://www.omdbapi.com/?apikey={key}&s={movieTitle}&page=";
                var validMovies = new List<DowloadedMovieInMovieListModel>();

                for (int i = 1; i <= pages; i++)
                {
                    var json_data = wc.DownloadString(uriForPage + i);
                    searchService.SerializeMovieList(json_data);
                    validMovies.AddRange(searchService.ValidateMovieList());
                }

                var movies = searchService.CreatePagedLinkedMovieList(validMovies, movieTitle, paginationParameters);

                return Ok(movies);
            }
        }
    }
}