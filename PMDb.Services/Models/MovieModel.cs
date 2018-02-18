using PMDb.Domain.Core;
using System;
using System.Collections.Generic;

namespace PMDb.Services.Models
{
    public class MovieModel
    {
        public string Title { get; set; }
        public int? Year { get; set; }
        public string Poster { get; set; }
        public string IMDbId { get; set; }
        public List<GenreModel> Genres { get; set; }
        public List<DirectorModel> Directors { get; set; }
        public List<WriterModel> Writers { get; set; }
        public List<ActorModel> Actors { get; set; }
        public List<TagModel> Tags { get; set; }
        public RatingModel Ratings { get; set; }
    }
}