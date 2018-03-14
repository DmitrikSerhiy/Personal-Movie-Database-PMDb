using Microsoft.AspNetCore.Mvc;
using PMDb.Domain.Core;
using PMDb.Services;
using PMDb.Services.Helpers;
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
        public IActionResult FilterMovies(MovieFilters movieFilters)
        {
            filtrationService.Filter(movieFilters);

            return NoContent();
        }





    }
}
