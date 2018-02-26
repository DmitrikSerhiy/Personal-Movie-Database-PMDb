using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDb.Services;
using Microsoft.AspNetCore.Mvc;
using PMDb.Domain.Interfaces;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using Microsoft.AspNetCore.JsonPatch;
using PMDb.Services.Helpers;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/movies")]
    public class MovieController : Controller
    {
        private IMovieService movieService;

        public MovieController(IMovieService MovieService)
        {
            movieService = MovieService;
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Getmovie(int id)
        {
            if (!movieService.IsMovieExist(id))
                return NotFound();
            
            //var validator = new MovieValidation();
            var movieModel = movieService.GetMovie(id);
            // var mov = new Movie
            //var validationResult = validator.Validate(movie);
            
            if (movieModel == null)
            {
                return (NotFound());
            }
            return Ok(movieModel);
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult GetMovies(GetMoviesParameters getMoviesParameters)
            //instead of specifying parameters directly
        {
            var movieModels = movieService.GetMovies(getMoviesParameters);
            if (movieModels == null)
            {
                return (NotFound());
            }
            return Ok(movieModels);
        }

        [HttpPatch("{id}", Name = "UpdateMark")]
        public IActionResult UpdateMark(int id,
            [FromBody] JsonPatchDocument<RatingModel> raitingDoc)
        {
            if (raitingDoc == null)
                return BadRequest();

            if (!movieService.IsMovieExist(id))
                return NotFound();

            var RatingsToPatch = movieService.GetMovie(id).Ratings;

            //redundant code by far
            if (RatingsToPatch == null)
                return NotFound();

            //add validation for ratingDoc

            raitingDoc.ApplyTo(RatingsToPatch);
            movieService.UpdateMark(id, RatingsToPatch.Mark);
           return NoContent();
        }

        [HttpDelete("{title}", Name = "DeleteMark")]
        public IActionResult DeleteMark(string title)
        {
            if (!movieService.IsMovieExist(title))
                return NotFound();

            // add validation for title
            movieService.DeleteMark(title);
            return NoContent();
        }


    }
}
