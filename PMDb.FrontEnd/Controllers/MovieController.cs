﻿using PMDb.Services;
using Microsoft.AspNetCore.Mvc;
using PMDb.Services.Models;
using Microsoft.AspNetCore.JsonPatch;
using PMDb.Services.Helpers;
using PMDb.Domain.Core;
using PMDb.Services.ServicesAbstraction;
using System.Collections.Generic;

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

        [HttpGet("{title}", Name = "GetMovie")]
        public IActionResult Getmovie(string title)
        {
            if (!movieService.IsMovieExist(title))
                return NotFound();

            var movieModel = movieService.GetMovie(title);

            if (movieModel == null)
                return (NotFound());

            return Ok(movieModel);
        }

        [HttpPatch("{title}/mark/{mark}", Name = "AddMark")]
        public IActionResult AddMark(string title, double mark)
        {
            if (movieService.IsMarkValid(mark) != true)
                return BadRequest();
            if (movieService.IsMovieExist(title) != true) 
                return NotFound();

            movieService.AddMark(mark, title);

            return NoContent();
        }

        [HttpPost(Name = "AddMovie")]
        public IActionResult AddMovie([FromBody]MovieModel movieModel)
        {
            if (movieModel == null)
                return NotFound();

            if (movieService.IsMovieExist(movieModel.Title))
                return NotFound();

            movieService.MapToMovie(movieModel);
            movieService.AddMovie();

            return CreatedAtRoute("GetMovie", new { title = movieService.GetName() }, movieModel);
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult GetMovies(PaginationParameters getMoviesParameters)
        {
            var movieModels = movieService.GetMovies(getMoviesParameters);
            //get library
            if (movieModels == null)
                return (NotFound());
            
            return Ok(movieModels);
        }

        [HttpPatch("{MovieTitleAddRatingFor}/mark", Name = "UpdateMark")]
        public IActionResult UpdateMark(string MovieTitleAddRatingFor,
            [FromBody] JsonPatchDocument<RatingModel> raitingDoc)
        {
            if (raitingDoc == null)
                return BadRequest();

            if (!movieService.IsMovieExist(MovieTitleAddRatingFor))
                return NotFound();


            var ratingModel = movieService.GetMovie(MovieTitleAddRatingFor).Ratings;

            raitingDoc.ApplyTo(ratingModel);

            if (!movieService.IsMarkValid(ratingModel.Mark))
                return BadRequest();

            movieService.UpdateMark(MovieTitleAddRatingFor, ratingModel.Mark);
            return NoContent();
        }

        [HttpPatch("{MovieTitleAddReviewFor}/review", Name = "EditReview")]
        public IActionResult EditReview(string MovieTitleAddReviewFor,//the same as add review
            [FromBody] JsonPatchDocument<MovieModel> movieDoc)
        {
            if (movieDoc == null)
                return BadRequest();

            if (!movieService.IsMovieExist(MovieTitleAddReviewFor))
                return NotFound();

            var movieToPatch = movieService.GetMovie(MovieTitleAddReviewFor);

            movieDoc.ApplyTo(movieToPatch);

            if (!movieService.IsReviewValid(movieToPatch))
                return BadRequest();

            movieService.EditReview(MovieTitleAddReviewFor, movieToPatch.Review);
            return NoContent();
        }

        [HttpDelete("{MovieTitleDeleteReviewFor}/review", Name = "DeleteReview")]
        public IActionResult DeleteReview(string MovieTitleDeleteReviewFor)
        {
            if (!movieService.IsMovieExist(MovieTitleDeleteReviewFor))
                return NotFound();

            movieService.DeleteReview(MovieTitleDeleteReviewFor);

            return NoContent();
        }

        [HttpDelete("{movieTitleDeliteMarkFor}/mark", Name = "DeleteMark")]
        public IActionResult DeleteMark(string movieTitleDeliteMarkFor)
        {
            if (!movieService.IsMovieExist(movieTitleDeliteMarkFor))
                return NotFound();

            movieService.DeleteMark(movieTitleDeliteMarkFor);
            return NoContent();
        }

        [HttpPatch("{movieTitle}/tags")]
        public IActionResult AddTags(string movieTitle, TagParameters tags)
        {
            if (!movieService.IsMovieExist(movieTitle))
                return NotFound();

            movieService.AddTags(tags, movieTitle);


            return NoContent();
        }

        [HttpDelete("{movieTitle}/tags", Name = "DeleteTags")]
        public IActionResult DeleteTags(string movieTitle, TagParameters tags)
        {
            if (!movieService.IsMovieExist(movieTitle))
                return NotFound();

            movieService.DeleteTag(tags, movieTitle);

            return NoContent();
        }

        [HttpDelete("{movieTitle}", Name ="DeleteMovie")]
        public IActionResult DeleteMovie(string movieTitle)
        {
            if (!movieService.IsMovieExist(movieTitle))
                return NotFound();

            movieService.DeleteMovie(movieTitle);

            return NoContent();
        }
    }
}
