using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class DownloadedMovieListModel
    {
        public DowloadedMovieInMovieListModel[] Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }
}
