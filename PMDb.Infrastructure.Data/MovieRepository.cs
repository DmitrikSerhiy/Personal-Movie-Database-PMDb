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

        public MovieRepository(MovieContext Context)
        {
            context = Context;
        }
        public void AddMark(double mark)
        {
            throw new NotImplementedException();
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

        public IQueryable<Movie> GetMovies()
        {
            var MovieCollectionBeforePaging = context.Movies
                   .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
                   .Include(r => r.Rating)
                   .OrderBy(m => m.Title);

            return MovieCollectionBeforePaging; //that's not the resource by far
           // return MovieCollectionBeforePaging;
            //return context.Movies
            //    .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
            //    .Include(r => r.Rating)
            //    .OrderBy(m => m.Title)
            //    .Skip(PageSize * (PageNumber - 1))
            //    .Take(PageSize)
            //    .ToList();
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

        public void UpdateMark(int movieId, double newMark)
        {
            context.Ratings
                .FirstOrDefault(m => m.MovieId == movieId)
                .OwnRating = newMark;
        }
    }
}
