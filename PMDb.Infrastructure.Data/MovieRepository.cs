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
        private MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }
        public void AddMark(double mark)
        {
            throw new NotImplementedException();
        }

        public void DeleteMark(int movieId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Movie GetMovie(int movieId)
        {
            return _context.Movies
                .Include(mg => mg.MovieGenre).ThenInclude(g => g.Genre)
                .Include(ma => ma.MovieActor).ThenInclude(a => a.Actor)
                .Include(ma => ma.MovieDirector).ThenInclude(d => d.Director)
                .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
                .Include(mw => mw.MovieWriter).ThenInclude(w => w.Writer)
                .Include(r => r.Rating)
                .FirstOrDefault(m => m.Id == movieId);
        }

        public IList<Movie> GetMovies()
        {
            return _context.Movies
                .Include(ma => ma.MovieTag).ThenInclude(t => t.Tag)
                .Include(r => r.Rating)
                .ToList();
        }

        public bool IsExist(int movieId)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == movieId) == null ? false : true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateMark(int movieId, double newMark)
        {
            _context.Ratings
                .FirstOrDefault(m => m.MovieId == movieId)
                .OwnRating = newMark;
        }
    }
}
