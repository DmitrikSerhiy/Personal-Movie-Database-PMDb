using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Core
{
    public class MovieListMovie
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int MovieListId { get; set; }
        public MovieList MovieList { get; set; }
    }
}
