using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class DowloadedMovieInMovieListModel
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
