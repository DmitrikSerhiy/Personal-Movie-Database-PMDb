using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Core
{
    public class Rating
    {
        public int Id { get; set; }
        public double? IMDbRating { get; set; }
        public int? PMDbVotes { get; set; }
        public double? RotenTomatosRaing { get; set; }
        public double? MetaCriticRating { get; set; }
        public double? OwnRating { get; set; }
        public int MovieId { get; set; }
        public  Movie Movie { get; set; } 
    }
}
