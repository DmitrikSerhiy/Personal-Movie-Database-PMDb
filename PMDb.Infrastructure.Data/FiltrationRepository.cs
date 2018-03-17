using Microsoft.EntityFrameworkCore;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Helpers;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class FiltrationRepository : IFiltrationRepository
    {
        private MovieContext context;
        FilterTransformer filterTransformer;
        FilterListСomposer filterListСomposer;
        private IEnumerable<Movie> movies = null;
        private IEnumerable<Actor> actors = null;
        private IEnumerable<Director> directors = null;
        private IEnumerable<Genre> genres = null;
        private IEnumerable<Writer> writers = null;
        private IEnumerable<Tag> tags = null;
        private IEnumerable<Rating> ownRatings = null;
        private IEnumerable<Rating> IMDbRatings = null;
        private IEnumerable<Rating> MetaCriticRatings = null;
        private IEnumerable<Rating> RotenTomatosRatings = null;
        private IEnumerable<Movie> FiltredMovie;


        public FiltrationRepository(MovieContext Context,
            FilterTransformer FilterTransformer)
        {
            context = Context;
            filterTransformer = FilterTransformer;
            filterListСomposer = new FilterListСomposer(context.Movies);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IList<Movie> Filter(MovieFilters filters)
        {
            filterTransformer.Transform(filters);
            foreach (var filter in filterTransformer.TransformedFilters)
            {
                switch (filter.Item2)
                {
                    case "Year":
                        movies = context.Movies.Include(ma => ma.MovieActor).Where(FilterQueryBuilder.BuildQuery<Movie>(filter)).ToList(); break;
                    case "Actor":
                        actors = context.Actors.Include(ma => ma.MovieActor).Where(FilterQueryBuilder.BuildQuery<Actor>((filter.Item1, nameof(Actor.Name)))).ToList(); break;
                    case "Director":
                        directors = context.Directors.Include(md => md.MovieDirector).Where(FilterQueryBuilder.BuildQuery<Director>((filter.Item1, nameof(Director.Name)))).ToList(); break;
                    case "Genre":
                        genres = context.Genres.Include(mg => mg.MovieGenre).Where(FilterQueryBuilder.BuildQuery<Genre>((filter.Item1, nameof(Genre.Name)))).ToList(); break;
                    case "Writer":
                        writers = context.Writers.Include(mw => mw.MovieWriter).Where(FilterQueryBuilder.BuildQuery<Writer>((filter.Item1, nameof(Writer.Name)))).ToList(); break;
                    case "Tag":
                        tags = context.Tags.Include(mt => mt.MovieTag).Where(FilterQueryBuilder.BuildQuery<Tag>((filter.Item1, nameof(Tag.Name)))).ToList(); break;
                    case "OwnRating":
                        ownRatings = context.Ratings.Where(FilterQueryBuilder.BuildQuery<Rating>(filter)).ToList(); break;
                    case "IMDbRating":
                        IMDbRatings = context.Ratings.Where(FilterQueryBuilder.BuildQuery<Rating>(filter)).ToList(); break;
                    case "MetaCriticRating":
                        MetaCriticRatings = context.Ratings.Where(FilterQueryBuilder.BuildQuery<Rating>(filter)).ToList(); break;
                    case "RotenTomatosRating":
                        RotenTomatosRatings = context.Ratings.Where(FilterQueryBuilder.BuildQuery<Rating>(filter)).ToList(); break;
                    default:
                        break;
                }
            }

            var smth = filterListСomposer.
                GetFiltredMovieList(Actors: actors,
                                    Movies: movies,
                                    Directors: directors,
                                    Genres: genres,
                                    Writers: writers,
                                    Tags: tags,
                                    OwnRatings: ownRatings,
                                    IMDbRatings: IMDbRatings,
                                    MetaCriticRatings: MetaCriticRatings,
                                    RotenTomatosRatings: RotenTomatosRatings).ToList();

            var fromClass = FiltredMovie.ToList();
            //this cause a huge performance 

            return null;
        }
        public void CombineLists(IEnumerable<Movie> filters)
        {
            if (filters != null)
            {
                FiltredMovie = FiltredMovie.Intersect(filters);
            }
        }


        public IQueryable<Movie> GetMovies()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int movieId)
        {
            return context.Movies.FirstOrDefault(m => m.Id == movieId) == null ? false : true;
        }

        public bool IsExist(string movieName)
        {
            return context.Movies.FirstOrDefault(m => m.Title == movieName) == null ? false : true;
        }
    }
}
