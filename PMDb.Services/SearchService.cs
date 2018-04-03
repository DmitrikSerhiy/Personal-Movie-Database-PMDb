using Newtonsoft.Json;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public class SearchService : ISearchService
    {
        private DownloadedMovieModel serializedMovie;
        private MovieModel mappedMovie;

        public SearchService()
        {
            
        }

        public void Serialize(string MovieString)
        {
            serializedMovie = JsonConvert
                .DeserializeObject<DownloadedMovieModel>(MovieString);
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
