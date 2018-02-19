using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public interface IMovieService
    {
        MovieModel GetMovie(int Id);
        IList<MovieModel> GetMovies();
        bool IsMovieExist(int movieId);
        void UpdateMark(int movieId, double newMark);
    }
}
