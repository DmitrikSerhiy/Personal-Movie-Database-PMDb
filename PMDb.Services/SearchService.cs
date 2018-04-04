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

        public SearchService()
        {
            failures = new List<string>();
        }

        public void Serialize(string MovieString)
        {
            serializedMovie = JsonConvert
                .DeserializeObject<Models.DownloadedMovieModel>(MovieString);

            Validate();
        }

        public void Validate()
        {
            DownloadedMovieModelValidator validator = 
                new DownloadedMovieModelValidator();
            var results = validator.Validate(serializedMovie);
            results.Errors[0].1
            if (results.IsValid != true)
                foreach (var item in results.Errors)
                {
                    if(item.PropertyName == "strings")
                    {
                        serializedMovie.//fack it!!!!!
                    }
                }
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
