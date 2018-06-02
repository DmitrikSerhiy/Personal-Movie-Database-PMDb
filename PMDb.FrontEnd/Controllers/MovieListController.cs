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
        public IActionResult CreateMovieList(string Name, [FromQuery] bool isDefault = false)
        {
            if (movieListService.IsMovieListExist(Name))
                //check whether movieList with such name is in db already
                return BadRequest();//409 needs

            var movieList = movieListService.CreateMovieList(Name, isDefault);
            return CreatedAtRoute("GetMovieList", new { Name }, movieList);
        }

        [HttpPost("{MovieListName}/{MovieTitle}", Name = "AddMovieToList")]
        public IActionResult AddMovieToList(string MovieListName, string MovieTitle)
        {
            if (movieListService.IsMovieListExist(MovieListName) != true)
                return NotFound();
            if (movieListService.IsMovieExist(MovieTitle) != true)
                return NotFound();
            if (movieListService.IsMovieExistInList(MovieListName, MovieTitle))
                return BadRequest();

            var movieList = movieListService.AddMovieToList(MovieListName, MovieTitle);

            return CreatedAtRoute("GetMovieList", new { Name = MovieListName }, movieList);
        }

        [HttpDelete("{MovieListName}/{MovieTitle}")]
        public IActionResult DeleteMovieFromList(string MovieListName, string MovieTitle)
        {
            if (movieListService.IsMovieListExist(MovieListName) != true)
                return NotFound();
            if (movieListService.IsMovieExistInList(MovieListName, MovieTitle) != true)
                return BadRequest();

            var movieList = movieListService.DeleteMovieFromList(MovieTitle, MovieListName);

            return NoContent();
        }

        [HttpDelete("{Name}")]
        public IActionResult DeleteMovieList(string Name)
        {
            if (movieListService.IsMovieListExist(Name) != true)
                return NotFound();

            movieListService.DeleteMovieList(Name);
            //movieListService.DeleteDefaultMovieList(Name);

            return NoContent();
        }

        [HttpPatch("{OldName}/{NewName}")]
        public IActionResult UpdateName(string OldName, string NewName)
        {
            if(movieListService.IsMovieListExist(OldName) != true)
                return NotFound();

            if (movieListService.IsMovieListExist(NewName))
                return BadRequest();//is already in db

            var movieList = movieListService.UpdateMovieListName(OldName, NewName);

            return NoContent();



        }

    }
}