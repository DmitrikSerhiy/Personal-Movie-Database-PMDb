using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using PMDb.Services.Helpers;
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
        private DownloadedMovieModel serializedMovie;
        private DownloadedMovieListModel serilizedMovieList;
        public List<DowloadedMovieInMovieListModel> Movies { get; set; } = new List<DowloadedMovieInMovieListModel>();
        public int TotalMovies = 0;
        private MovieModel mappedMovie;
        private List<string> failures;
        private ILinksGenerator<LinkedResourceBase, PaginationParameters> linksGenetator;
        DownloadedMovieModelValidator validator;

        public SearchService(ILinksGenerator<LinkedResourceBase, PaginationParameters> LinksGenetator)
        {
            failures = new List<string>();
            validator = new DownloadedMovieModelValidator();
            linksGenetator = LinksGenetator;
        }

        public void SerializeMovie(string MovieString)
        {
            serializedMovie = JsonConvert
                .DeserializeObject<DownloadedMovieModel>(MovieString);
        }
        public void SerializeMovieList(string MovieString)
        {
            serilizedMovieList = JsonConvert.DeserializeObject<DownloadedMovieListModel>(MovieString);
            Int32.TryParse(serilizedMovieList.totalResults, out TotalMovies);
        }

        public int GetMovieAmount()
        {
            return TotalMovies;
        }
        public int CalculatePages(int moviesAmount)
        {
            return (int)Math.Ceiling(moviesAmount / (double)10);
        }
        public void ValidateMovie()
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

        public List<DowloadedMovieInMovieListModel> ValidateMovieList()
        {
            var validMovies = new List<DowloadedMovieInMovieListModel>();
            foreach (var movie in serilizedMovieList.Search)
                if(movie.Poster != "N/A" && movie.Type != "game") validMovies.Add(movie);
            return validMovies;
        }

        public void MapToMovieModel()
        {
            mappedMovie = DownloadedMovieMaper.Map(serializedMovie);
        }
        public MovieModel GetMovie()
        {
            return mappedMovie;
        }
        public List<DowloadedMovieInMovieListModel> GetMovies()
        {
            return Movies;
        }

        public SearchedMovieListModel CreatePagedLinkedMovieList(List<DowloadedMovieInMovieListModel> composedMovies,
            string movieTitle, PaginationParameters paginationParameters)
        {
            var padedMovieList = new SearchedMovieListModel();

            padedMovieList.Movies = PagedList<DowloadedMovieInMovieListModel>.Create(composedMovies,
                paginationParameters.PageNumber,
                paginationParameters.PageSize);

            padedMovieList.searchedMovie = movieTitle;
            padedMovieList.totalMovies = padedMovieList.Movies.TotalCount;
            padedMovieList.totalPages = padedMovieList.Movies.TotalPages;
            padedMovieList.LinksForPagination = new List<LinkModel>();
            padedMovieList.LinksForPagination.AddRange(linksGenetator
                .CreateLinksForSearchedMovieListPages(padedMovieList, paginationParameters));

            return padedMovieList;
            //padedMovieList.Links.Add

        }

        public bool IsExist()
        {
            return serializedMovie.Response;
        }
    }
}
