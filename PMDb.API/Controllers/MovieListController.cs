using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMDb.Services.ServicesAbstraction;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/movieList")]
    public class MovieListController : Controller
    {
        IMovieListService movieListService;
        public MovieListController(IMovieListService MovieListService)
        {
            movieListService = MovieListService;
        }


        [HttpGet("{Name}", Name ="GetMovieList")]
        public IActionResult GetMovieList(string Name)
        {
            if (movieListService.IsMovieListExist(Name) == false)
                return NotFound();

            var movieList = movieListService.GetMovieList(Name);
            return Ok(movieList);
        }

        [HttpPost("{Name}")]
        public IActionResult CreateMovieList(string Name, [FromRoute] bool isDefault = false)
        {
            if (movieListService.IsMovieListExist(Name))
                //check whether movieList with such name is in db alredy
                return BadRequest();//409 needs

            var movieList = movieListService.CreateMovieList(Name, false);
            return CreatedAtRoute("GetMovieList", new { Name }, movieList);
        }
    }
}