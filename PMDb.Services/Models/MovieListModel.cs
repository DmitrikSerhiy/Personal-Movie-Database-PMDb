using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class MovieListModel
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public IList<SimplifiedMovieModel> MovieListMovies { get; set; }
    }
}
