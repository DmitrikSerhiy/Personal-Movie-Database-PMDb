using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Core
{
    public class MovieList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public IList<MovieListMovie> MovieListMovies { get; set; }

        public MovieList()
        {
            MovieListMovies = new List<MovieListMovie>();
        }
    }
}
