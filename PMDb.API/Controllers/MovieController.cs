using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDb.Services;
using Microsoft.AspNetCore.Mvc;
using PMDb.Domain.Interfaces;
using PMDb.Services.Mappers;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
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
            var validator = new MovieValidation();

            var movieModel = movieService.GetMovie(id);

           // var mov = new Movie
            //var validationResult = validator.Validate(movie);
            if (movieModel == null)
            {
                return (NotFound());
            }



            return Ok(movieModel);

        }
        //    // GET: api/Movie
        //    [HttpGet]
        //    public IEnumerable<string> Get()
        //    {
        //        return new string[] { "value1", "value2" };
        //    }



        //// POST: api/Movie
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Movie/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
