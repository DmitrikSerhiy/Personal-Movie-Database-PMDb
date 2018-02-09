using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDb.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMDb.Domain.Interfaces;

namespace PMDb.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Getmovie(int id)
        {
            var validator = new MovieValidation();

            var movie = _movieRepository.GetMovie(id);

            //var validationResult = validator.Validate(movie);
            if (movie == null)
            {
                return (NotFound());
            }

            var movieModel = MovieMapper.Map(movie);

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
