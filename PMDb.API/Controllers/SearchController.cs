using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMDb.Services;
using PMDb.Services.ServicesAbstraction;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private ISearchService searchService;

        public SearchController(ISearchService SearchService)
        {
            searchService = SearchService;
        }
        [HttpGet("{title}")]
        public IActionResult FindMovie(string title)
        {
            var key = "f6a55b6a";
            var uriString = $"http://www.omdbapi.com/?apikey={key}&t={title}";
            Uri targetUri = new Uri(uriString);

            using (var w = new WebClient())
            {
                var json_data = string.Empty;

                json_data = w.DownloadString(uriString);
                searchService.Serialize(json_data);
                searchService.Validate();
                if (!searchService.IsExist()){
                    return NotFound();
                }

                searchService.MapToModel();
                var movie = searchService.GetMovie();

                return Ok(movie);
            }
        }
    }
}