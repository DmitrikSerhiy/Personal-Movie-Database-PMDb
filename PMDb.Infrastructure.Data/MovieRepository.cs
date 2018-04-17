using Microsoft.EntityFrameworkCore;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PMDb.Infrastructure.Data
{
    public class MovieRepository : IMovieRepository

    {
        private MovieContext context;
        private DuplicateChecker duplicateChecker;

        public MovieRepository(MovieContext Context, DuplicateChecker DuplicateChecker)
        {
            context = Context;
            duplicateChecker = DuplicateChecker;
        }


        public void AddMovie(Movie movie)
        {
            context.Movies.Add(movie);
        }

        public void InitExistedEntities(Movie movie)
        {
            duplicateChecker.CheckAndInitActors(movie.MovieActor);
            duplicateChecker.CheckAndInitDirectors(movie.MovieDirector);
            duplicateChecker.CheckAndInitGenres(movie.MovieGenre);
            duplicateChecker.CheckAndInitTags(movie.MovieTag);
            duplicateChecker.CheckAndInitWriters(movie.MovieWriter);
        }

        public void AddReview(string movieName, string review)
        {
            var movie = context.Movies
                .Where(m => m.Title == movieName)
                .SingleOrDefault();

            context.Entry(movie).Property(nameof(Movie.Review))
                .CurrentValue = review;
        }

        public void AddMark(double mark, string movieTitle)
        {
            var movie = context.Movies
                .Include(r => r.Rating)
                .FirstOrDefault(m => m.Title == movieTitle);
            movie.Rating.OwnRating = mark;
        }

        public void DeleteMark(string movieName)
        {
            var movie = context.Movies.FirstOrDefault(m => m.Title == movieName);
            context.Movies.Remove(movie);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Movie GetMovie(int movieId)
        {
            return context.Movies
                .Include(mg => mg.MovieGenre).ThenInclude(g => g.Genre)
                .Include(ma => ma.MovieActor).ThenInclude(a => a.Actor)
                .Include(ma => ma.MovieDirector).ThenInclude(d => d.Director)
                .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
                .Include(mw => mw.MovieWriter).ThenInclude(w => w.Writer)
                .Include(r => r.Rating)
                .FirstOrDefault(m => m.Id == movieId);
        }

        public Movie GetMovie(string title)
        {
            return context.Movies
               .Include(mg => mg.MovieGenre).ThenInclude(g => g.Genre)
               .Include(ma => ma.MovieActor).ThenInclude(a => a.Actor)
               .Include(ma => ma.MovieDirector).ThenInclude(d => d.Director)
               .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
               .Include(mw => mw.MovieWriter).ThenInclude(w => w.Writer)
               .Include(r => r.Rating)
               .FirstOrDefault(m => m.Title == title);
        }

        public IQueryable<Movie> GetMovies()
        {
            return context.Movies
                   .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
                   .Include(r => r.Rating)
                   .OrderBy(m => m.Title);
        }

        public int GetId(Movie movie)
        {
            return context.Movies.FirstOrDefault(m => m.Id == movie.Id).Id;
        }

        public bool IsExist(int movieId)
        {
            return context.Movies.FirstOrDefault(m => m.Id == movieId) == null ? false : true;
        }

        public bool IsExist(string movieName)
        {
            return context.Movies.FirstOrDefault(m => m.Title == movieName) == null ? false : true;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateMark(string movieName, double newMark)
        {
            var ratingEntity = context.Movies
                .Where(m => m.Title == movieName)
                .Select(m => m.Rating)
                .SingleOrDefault();

            context.Entry(ratingEntity)
                .Property(nameof(Rating.OwnRating)).CurrentValue = newMark;
        }
    }
}
