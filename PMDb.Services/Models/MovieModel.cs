using PMDb.Domain.Core;
using System;
using System.Collections.Generic;

namespace PMDb.Services.Models
{
    public class MovieModel
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
        public string IMDbId { get; set; }
        public string Runtime { get; set; }
        public string Plot { get; set; }
        public string Review { get; set; }
        public List<GenreModel> GenreModels { get; set; }
        public List<DirectorModel> DirectorModels { get; set; }
        public List<WriterModel> WriterModels { get; set; }
        public List<ActorModel> ActorModels { get; set; }
        public List<TagModel> TagModels { get; set; }
        public RatingModel Ratings { get; set; }
    }
}