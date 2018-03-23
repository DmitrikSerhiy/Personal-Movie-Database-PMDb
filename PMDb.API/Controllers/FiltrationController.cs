using Microsoft.AspNetCore.Mvc;
using PMDb.Domain.Core;
using PMDb.Services;
using PMDb.Services.Helpers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/movies/filter")]
    public class FiltrationController : Controller
    {
        private IFiltrationService filtrationService;


        public FiltrationController(IFiltrationService FiltrationService)
        {
            filtrationService = FiltrationService;
        }


        [HttpGet(Name = "FilterMovies")]
        public IActionResult FilterMovies(MovieFilters movieFilters,
            PaginationParameters paginationParameters)
        {
            filtrationService.Filter(movieFilters);
            var filtredMovies = filtrationService.GetFiltredMovies(paginationParameters)
                as PagedList<SimplifiedMovieModel>;

            if(filtredMovies == null)
            {
                return NotFound();
            }

            var paginationMetada = new
            {
                totalCount = filtredMovies.TotalCount,
                pageSize = filtredMovies.PageSize,
                currentPage = filtredMovies.CurrentPage,
                totalPages = filtredMovies.TotalPages,
                previousPageLink = filtrationService.GeneratePreviousPageLink(filtredMovies.HasPrevious, paginationParameters),
                nextPageLink = filtrationService.GenerateNextPageLink(filtredMovies.HasNext, paginationParameters)
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetada));

            return Ok(filtredMovies);
        }





    }
}
