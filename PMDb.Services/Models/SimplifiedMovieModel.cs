using PMDb.Domain.Core;
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
        public string Review { get; set; }
        public List<ListOfMovieListsModel> ListsWithCurrMovie { get; set; }
        public bool IsInWatchLater { get; set; }
        public bool IsInFavoriteList { get; set; }
        public bool HasTags { get; set; }
        public bool HasReview { get; set; }

    }
}
