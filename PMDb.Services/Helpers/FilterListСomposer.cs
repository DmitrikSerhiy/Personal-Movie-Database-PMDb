using Microsoft.EntityFrameworkCore;
using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDb.Services.Helpers
{
    public class FilterListСomposer
    {
        private DbSet<Movie> MoviesFromDb;
        private IEnumerable<Movie> FiltredMovie;

        public FilterListСomposer(DbSet<Movie> MoviesToFilter)
        {
            MoviesFromDb = MoviesToFilter;
            FiltredMovie = MoviesFromDb;
        }

        public void CombineLists(IEnumerable<Movie> filters)
        {
            if (filters != null)
            {
                FiltredMovie = FiltredMovie.Intersect(filters);
            }
        }

        //that's super redundant and bad written code
        //I MUST REWRITE QueryBuilder THUS THIS CODE DISAPEAR!!!
        public IEnumerable<Movie> GetFiltredMovieList(
                    IEnumerable<Actor> Actors = null,
                    IEnumerable<Director> Directors = null,
                    IEnumerable<Movie> Movies = null,
                    IEnumerable<Genre> Genres = null,
                    IEnumerable<Writer> Writers = null,
                    IEnumerable<Tag> Tags = null,
                    IEnumerable<Rating> OwnRatings = null,
                    IEnumerable<Rating> IMDbRatings = null,
                    IEnumerable<Rating> MetaCriticRatings = null,
                    IEnumerable<Rating> RotenTomatosRatings = null)
        {
            if(Actors != null) CombineLists(FilterByActors(Actors));
            if (Directors != null) CombineLists(FilterByDirectors(Directors));
            if (Movies != null) CombineLists(FilterByYears(Movies));
            if (Writers != null) CombineLists(FilterByWriters(Writers));
            if (Tags != null) CombineLists(FilterByTags(Tags));
            if (Genres != null) CombineLists(FilterByGenres(Genres));
            if (OwnRatings != null) CombineLists(FilterByOwnRatings(OwnRatings));
            if (IMDbRatings != null) CombineLists(FilterByIMDbRatings(IMDbRatings));
            if (MetaCriticRatings != null) CombineLists(FilterByMetaCriticRatings(MetaCriticRatings));
            if (RotenTomatosRatings != null) CombineLists(FilterByRotenTomatosRatings(RotenTomatosRatings));
            return FiltredMovie;
        }

        private IEnumerable<Movie> FilterByActors(IEnumerable<Actor> Actors)
        {
            return MoviesFromDb.Where(movie =>
                Actors.All(requiredActor =>
                movie.MovieActor.Any(movieActor =>
                movieActor.Actor == requiredActor)));
        }

        private IEnumerable<Movie> FilterByDirectors(IEnumerable<Director> Directors)
        {
            return MoviesFromDb.Where(movie =>
                Directors.All(requiredDirector =>
                movie.MovieDirector.Any(movieDirector =>
                movieDirector.Director == requiredDirector)));
        }

        private IEnumerable<Movie> FilterByYears(IEnumerable<Movie> Movies)
        {
            return MoviesFromDb.Where(movie =>
                Movies.All(m => m.Year == movie.Year));
        }

        private IEnumerable<Movie> FilterByWriters(IEnumerable<Writer> Writers)
        {
            return MoviesFromDb.Where(movie =>
                Writers.All(requiredWriter =>
                movie.MovieWriter.Any(movieWriter =>
                movieWriter.Writer == requiredWriter)));
        }

        private IEnumerable<Movie> FilterByTags(IEnumerable<Tag> Tags)
        {
            return MoviesFromDb.Where(movie =>
                Tags.All(requiredTag =>
                movie.MovieTag.Any(movieTag =>
                movieTag.Tag == requiredTag)));
        }

        private IEnumerable<Movie> FilterByGenres(IEnumerable<Genre> Genres)
        {
            return MoviesFromDb.Where(movie =>
                Genres.All(requiredGenre =>
                movie.MovieGenre.Any(movieGenre =>
                movieGenre.Genre == requiredGenre)));
        }

        private IEnumerable<Movie> FilterByOwnRatings(IEnumerable<Rating> OwnRatings)
        {
            return MoviesFromDb.Where(movie =>
                OwnRatings.All(requiredRating =>
                movie.Rating.OwnRating == requiredRating.OwnRating));
        }

        private IEnumerable<Movie> FilterByIMDbRatings(IEnumerable<Rating> IMDbRatings)
        {
            return MoviesFromDb.Where(movie =>
                IMDbRatings.All(requiredRating =>
                movie.Rating.IMDbRating == requiredRating.IMDbRating));
        }

        private IEnumerable<Movie> FilterByMetaCriticRatings(IEnumerable<Rating> MetaCriticRatings)
        {
            return MoviesFromDb.Where(movie =>
                MetaCriticRatings.All(requiredRating =>
                movie.Rating.MetaCriticRating == requiredRating.MetaCriticRating));
        }

        private IEnumerable<Movie> FilterByRotenTomatosRatings(IEnumerable<Rating> RotenTomatosRatings)
        {
            return MoviesFromDb.Where(movie =>
                RotenTomatosRatings.All(requiredRating =>
                movie.Rating.RotenTomatosRating == requiredRating.RotenTomatosRating));
        }
    }
}
