using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using PMDb.Services.ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Text;


namespace PMDb.Services
{
    public class SearchService : ISearchService
    {
        private Models.DownloadedMovieModel serializedMovie;
        private MovieModel mappedMovie;
        private List<string> failures;
        DownloadedMovieModelValidator validator;

        public SearchService()
        {
            failures = new List<string>();
            validator = new DownloadedMovieModelValidator();
        }

        public void Serialize(string MovieString)
        {
            serializedMovie = JsonConvert
                .DeserializeObject<Models.DownloadedMovieModel>(MovieString);
        }

        public void Validate()
        {
            var RatingsValidation = validator.Validate(serializedMovie, ruleSet: "Ratings");
            var IMDbRatingsValidation = validator.Validate(serializedMovie, ruleSet: "IMDbRatings");

            if (IMDbRatingsValidation.IsValid != true)
            {
                serializedMovie.imdbRating = "0.0";
                serializedMovie.imdbVotes = "0";
            }
            var cout = serializedMovie.Ratings.Length;


        }

        public void MapToModel()
        {
            mappedMovie = DownloadedMovieMaper.Map(serializedMovie);
        }
        public MovieModel GetMovie()
        {
            return mappedMovie;
        }

        public bool IsExist()
        {
            return serializedMovie.Response;
        }
    }
}
