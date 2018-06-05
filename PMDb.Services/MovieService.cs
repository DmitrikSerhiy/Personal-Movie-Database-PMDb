using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Helpers;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using PMDb.Services.ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace PMDb.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository movieRepository;
        //private LinksGenetator linksGenetator;
        private MovieModel MovieModel;
        public Movie movieToAdd;
        public double markToAdd;
        public IUrlHelper urlHelper;
        public MovieModelValidator validator;

        public MovieService(IMovieRepository MovieRepository, IUrlHelper UrlHelper
            //LinksGenetator LinksGenetator
            )
        {
            movieRepository = MovieRepository;
            urlHelper = UrlHelper;
            //linksGenetator = LinksGenetator;
            validator = new MovieModelValidator();
        }

        public void MapToModel(Movie movie)
        {
            MovieModel = MovieMapper.Map(movie);
        }
        public MovieModel GetMovie(int id)
        {
            var movie = movieRepository.GetMovie(id);
            MapToModel(movie);
            return MovieModel;
        }

        public MovieModel GetMovie(string title)
        {
            var movie = movieRepository.GetMovie(title);
            MapToModel(movie);
            //linksGenetator.CreateLinksForGetMovie(MovieModel, title);//bool shit
            InitBoolFields(ref MovieModel);
            return MovieModel;
        }

        public void MapToMovie(MovieModel movieModel)
        {
            movieToAdd = MovieMapper.Map(movieModel);
        }


        public void AddMovie()
        {
            movieRepository.InitExistedEntities(movieToAdd);
            movieRepository.AddMovie(movieToAdd);
            movieRepository.Save();
        }

        public void EditReview(string movieName, string review)
        {
            movieRepository.EditReview(movieName, review);
            movieRepository.Save();
        }

        public void DeleteReview(string movieName)
        {
            movieRepository.DeleteReview(movieName);
            movieRepository.Save();
        }

        public void AddTags(TagParameters tagParameters, string movieName)
        {
            var tags = FormTags(tagParameters.tag);
            tags = DeleteTagDuplicates(tags);
            var newtags = new List<Tag>();
            var tagsFromDb = new List<Tag>();
            var allTags = new List<Tag>();
            foreach (var tagName in tags)
            {
                if (movieRepository.IsTagAttachedToMovie(tagName, movieName) != true)
                {
                    if (movieRepository.IsTagExist(tagName) != true)
                        newtags.Add(new Tag() { Name = tagName });
                    else
                        tagsFromDb.Add(movieRepository.GetExistedTag(tagName));
                }
            }
            movieRepository.AddTagsToDb(newtags);
            allTags.AddRange(tagsFromDb);
            allTags.AddRange(newtags);
            movieRepository.AddTagsToMovie(allTags, movieName);
            movieRepository.Save();
        }

        public void DeleteTag(TagParameters tagParameters, string movieName)
        {
            var tags = FormTags(tagParameters.tag);
            foreach (var tag in tags)
            {
                if (movieRepository.IsTagAttachedToMovie(tag, movieName))
                {
                    movieRepository.DeleteTag(tag, movieName);
                }
            }
            movieRepository.Save();
        }

        public bool IsTagExist(string tagName)
        {
            return movieRepository.IsExist(tagName);
        }

        private IList<string> FormTags(IList<string> tags)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                tags[i] = "#" + tags[i];
            }
            return tags;
        }

        private IList<string> DeleteTagDuplicates(IList<string> tags)
        {
            HashSet<string> uniqueTags = new HashSet<string>(tags);
            return new List<string>(uniqueTags);
        }

        public int GetId()
        {
            return movieToAdd.Id;
        }

        public string GetName()
        {
            return movieToAdd.Title;
        }


        public IList<SimplifiedMovieModel> GetMovies(PaginationParameters getMoviesParameters)
        {
            var MovieCollectionBeforePaging = movieRepository.GetMovies();

            var movies = PagedList<Movie>.Create(MovieCollectionBeforePaging,
                getMoviesParameters.PageNumber,
                getMoviesParameters.PageSize);

            var PagedSimplifiedMovies = new PagedList<SimplifiedMovieModel>(
                movies.CurrentPage, movies.TotalPages, movies.PageSize,
                movies.TotalCount, movies.HasPrevious, movies.HasNext);

            foreach (var item in movies)
            {
                PagedSimplifiedMovies.Add(SimplifiedMovieMapper.Map(item));
            }

            InitBoolFields(ref PagedSimplifiedMovies);
            
            return PagedSimplifiedMovies;
        }

        public void InitBoolFields(ref PagedList<SimplifiedMovieModel> movies)
        {
            foreach (var movie in movies)
            {
                foreach (var list in movie.ListsWithCurrMovie)
                {
                    if (!movie.IsInWatchLater) movie.IsInWatchLater = list.MovieListName == "WatchLater";
                    if (!movie.IsInFavoriteList) movie.IsInFavoriteList = list.MovieListName == "Favorite";
                }
                movie.HasTags = movie.Tags.Count == 0 ? false : true;
                movie.HasReview = !String.IsNullOrEmpty(movie.Review);
            }
        }

        public void InitBoolFields(ref MovieModel movie)
        {
            foreach (var list in movie.ListsWithCurrMovie)
            {
                if(!movie.IsInWatchLater) movie.IsInWatchLater = list.MovieListName == "WatchLater";
                if (!movie.IsInFavoriteList) movie.IsInFavoriteList = list.MovieListName == "Favorite";
            }
            movie.HasTags = movie.TagModels.Count == 0 ? false : true;
            movie.HasReview = !String.IsNullOrEmpty(movie.Review);
        }


        public string GenerateNextPageLink(bool nextPage, PaginationParameters getMoviesParameters)
        {
            return nextPage ?
               UriProvider.CreateMoviesUri(getMoviesParameters,
               UriType.NextPage, urlHelper as UrlHelper) : null;
        }

        public string GeneratePreviousPageLink(bool previousPage, PaginationParameters getMoviesParameters)
        {
            return previousPage ?
               UriProvider.CreateMoviesUri(getMoviesParameters,
               UriType.PreviousPage, urlHelper as UrlHelper) : null;
        }

        public bool IsMovieExist(int movieId)
        {
            return movieRepository.IsExist(movieId);
        }

        public bool IsMovieExist(string movieName)
        {
            return movieRepository.IsExist(movieName);
        }

        public void AddMark(double mark, string movieTitle)
        {
            markToAdd = mark;
            movieRepository.AddMark(mark, movieTitle);
            movieRepository.Save();
            
        }

        public bool IsMarkValid(double mark)
        {
            MarkValidator MarkValidator = new MarkValidator();
            return MarkValidator.Validate(mark).IsValid;
        }

        public bool IsMarkValid(MovieModel movieModel)
        {
            return validator.Validate(movieModel, ruleSet: "Mark").IsValid;
        }

        public bool IsReviewValid(MovieModel movieModel)
        {
            return validator.Validate(movieModel, ruleSet: "Review").IsValid;
        }

        public void UpdateMark(string movieName, double newMark)
        {
            movieRepository.UpdateMark(movieName, newMark);
            movieRepository.Save();
        }

        public void DeleteMark(string movieName)
        {
            movieRepository.DeleteMark(movieName);
            movieRepository.Save();
        }

        public void DeleteMovie(string movieName)
        {
            movieRepository.DeleteMovie(movieName);
            movieRepository.Save();
        }
    }
}
