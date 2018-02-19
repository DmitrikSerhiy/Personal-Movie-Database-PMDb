using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Models
{
    public class RatingModel
    {
        public double? IMDbRating { get; set; }
        public int? IMDbVotes { get; set; }
        public double? RotenTomatosRating { get; set; }
        public double? MetaCriticRating { get; set; }
        public double Mark { get; set; }
    }
}
