using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class SimplifiedMovieModel
    {
        public string Title { get; set; }
        public int? Year { get; set; }
        public string Poster { get; set; }
        public double Mark { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}
