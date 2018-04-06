using PMDb.Services;
using Microsoft.AspNetCore.Mvc;
using PMDb.Services.Models;
using Microsoft.AspNetCore.JsonPatch;
using PMDb.Services.Helpers;
using PMDb.Domain.Core;
using PMDb.Services.ServicesAbstraction;

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

            //var validator = new MovieValidation();
            var movieModel = movieService.GetMovie(title);
            // var mov = new Movie
            //var validationResult = validator.Validate(movie);

            if (movieModel == null)
            {
                return (NotFound());
            }
            return Ok(movieModel);
        }

        [HttpPatch("{title}/{mark}")]
        public IActionResult AddMark(string title, double mark)
        {
            if (movieService.IsMarkValid() != true)
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
            {
                return BadRequest();
            }

            movieService.MapToMovie(movieModel);
            movieService.AddMovie();

            return CreatedAtRoute("GetMovie", new { id = movieService.GetId() }, movieModel);
        }

        [HttpDelete("{title}", Name = "DeleteMovie")]
        public IActionResult DeleteMovie(string title)
        {
            if(movieService.IsMovieExist(title) == false)
            {
                return NotFound();
            }

            movieService.DeleteMovie(title);

            return NoContent();
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult GetMovies(PaginationParameters getMoviesParameters)
        {
            var movieModels = movieService.GetMovies(getMoviesParameters) 
                as PagedList<SimplifiedMovieModel>;
            
            if (movieModels == null)
            {
                return (NotFound());
            }

            var paginationMetada = new
            {
                totalCount = movieModels.TotalCount,
                pageSize = movieModels.PageSize,
                currentPage = movieModels.CurrentPage,
                totalPages = movieModels.TotalPages,
                previousPageLink = movieService.GeneratePreviousPageLink(movieModels.HasPrevious, getMoviesParameters),
                nextPageLink = movieService.GenerateNextPageLink(movieModels.HasNext, getMoviesParameters)
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetada));

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

        [HttpDelete("{movieTitleDeliteMarkFor}", Name = "DeleteMark")]
        public IActionResult DeleteMark(string movieTitleDeliteMarkFor)
        {
            if (!movieService.IsMovieExist(movieTitleDeliteMarkFor))
                return NotFound();

            // add validation for title
            movieService.DeleteMark(movieTitleDeliteMarkFor);
            return NoContent();
        }


    }
}
