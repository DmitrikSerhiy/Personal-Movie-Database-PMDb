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
        public List<GenreModel> GenreModels { get; set; }
        public List<DirectorModel> DirectorModels { get; set; }
        public List<WriterModel> WriterModels { get; set; }
        public List<ActorModel> ActorModels { get; set; }
        public List<TagModel> TagModels { get; set; }
        public RatingModel Ratings { get; set; }
    }
}