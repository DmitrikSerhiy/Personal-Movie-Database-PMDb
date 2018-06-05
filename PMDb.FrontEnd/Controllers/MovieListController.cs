using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMDb.Services.Helpers;
using PMDb.Services.Models;
using PMDb.Services.ServicesAbstraction;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/movieList")]
    public class MovieListController : Controller
    {
        IMovieListService movieListService;
        ILinksGenerator<LinkedResourceBase, PaginationParameters> linksGenerator;
        //ILinksGenerator linksGenerator;
        public MovieListController(IMovieListService MovieListService,
            ILinksGenerator<LinkedResourceBase, PaginationParameters> LinksGenerator)
          // ILinksGenerator LinksGenerator)
        {
            movieListService = MovieListService;
            linksGenerator = LinksGenerator;
        }


        [HttpGet("{Name}", Name ="GetMovieList")]
        public IActionResult GetMovieList(string Name, PaginationParameters paginationParameters)
        {
            if (movieListService.IsMovieListExist(Name) == false)
                return NotFound();
            var movieList = movieListService.GetMovieList(Name, paginationParameters);
            movieList.Links.AddRange(linksGenerator.CreateLinksForMovieList(movieList, paginationParameters));
            return Ok(movieList);
        }

        [HttpPost("{Name}")]
        public IActionResult CreateMovieList(string Name, PaginationParameters PaginationParameters, [FromQuery] bool isDefault = false)
        {
            if (movieListService.IsMovieListExist(Name))
                //check whether movieList with such name is in db already
                return BadRequest();//409 needs

            var movieList = movieListService.CreateMovieList(Name, PaginationParameters, isDefault);
            return CreatedAtRoute("GetMovieList", new { Name }, movieList);
        }

        [HttpPost("{MovieListName}/{MovieTitle}", Name = "AddMovieToList")]
        public IActionResult AddMovieToList(string MovieListName, string MovieTitle, PaginationParameters PaginationParameters)
        {
            if (movieListService.IsMovieListExist(MovieListName) != true)
                return NotFound();
            if (movieListService.IsMovieExist(MovieTitle) != true)
                return NotFound();
            if (movieListService.IsMovieExistInList(MovieListName, MovieTitle))
                return BadRequest();

            var movieList = movieListService.AddMovieToList(MovieListName, MovieTitle, PaginationParameters);

            return CreatedAtRoute("GetMovieList", new { Name = MovieListName }, movieList);
        }

        [HttpDelete("{MovieListName}/{MovieTitle}")]
        public IActionResult DeleteMovieFromList(string MovieListName, string MovieTitle, PaginationParameters PaginationParameters)
        {
            if (movieListService.IsMovieListExist(MovieListName) != true)
                return NotFound();
            if (movieListService.IsMovieExistInList(MovieListName, MovieTitle) != true)
                return BadRequest();

            var movieList = movieListService.DeleteMovieFromList(MovieTitle, MovieListName, PaginationParameters);

            return Ok(movieList);
        }

        [HttpDelete("{Name}")]
        public IActionResult DeleteMovieList(string Name)
        {
            if (movieListService.IsMovieListExist(Name) != true)
                return NotFound();

            movieListService.DeleteMovieList(Name);
            //movieListService.DeleteDefaultMovieList(Name);//

            return NoContent();
        }

        [HttpPatch("{OldName}/{NewName}")]
        public IActionResult UpdateName(string OldName, string NewName, PaginationParameters PaginationParameters)
        {
            if(movieListService.IsMovieListExist(OldName) != true)
                return NotFound();

            if (movieListService.IsMovieListExist(NewName))
                return BadRequest();//is already in db

            var movieList = movieListService.UpdateMovieListName(OldName, NewName, PaginationParameters);

            return Ok(movieList);



        }

    }
}