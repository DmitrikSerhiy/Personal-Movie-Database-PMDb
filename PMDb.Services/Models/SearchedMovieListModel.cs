using PMDb.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class SearchedMovieListModel : LinkedResourceBase
    {
        public PagedList<DowloadedMovieInMovieListModel> Movies { get; set; }
        = new PagedList<DowloadedMovieInMovieListModel>();

        public string searchedMovie { get; set; }
        public int totalMovies { get; set; }
        public int totalPages { get; set; }
        //some fields right here
    }
}
