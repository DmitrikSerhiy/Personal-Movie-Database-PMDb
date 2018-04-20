using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class SimplifiedMovieModel : LinkedResourceBase
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
        public double Mark { get; set; }
        public string Runtime { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}
