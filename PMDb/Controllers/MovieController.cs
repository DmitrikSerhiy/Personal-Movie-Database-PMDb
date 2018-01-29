using Microsoft.AspNetCore.Mvc;
using PMDb.Domain.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMDb.Controllers
{
    [Route("api/movies")]
    public class MovieController : Controller
    {

        //private IMovieRepository _repository;
        //public MovieController(IMovieRepository repository)
       // {
       //     _repository = repository;
       // }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            //var movie = _repository.GetMovie(id);
            // return Ok(new JsonResult(movie));
            return Ok("looool");
        }

        [HttpPost("{mark}")]
        public IActionResult AddMark(double mark)
        {
   //         _repository.AddMark(mark);
            return Ok();
        }
    }
}
