using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie GetMovie(int movieId);
        Movie GetMovie(string title);
        void AddMovie(Movie movie);
        void AddMark(double mark, string movieTitle);
        int GetId(Movie movie);
        void UpdateMark(string movieName, double newMark);
        void Save();
        void DeleteMark(string movieName);
        void EditReview(string movieName, string review);
        void DeleteReview(string movieName);
        void InitExistedEntities(Movie movie);
        void AddTagsToDb(IList<Tag> tags);
        void AddTagsToMovie(IList<Tag> tags, string movieName);
        bool IsTagExist(string tag);
        Tag GetExistedTag(string tag);
        bool IsTagAttachedToMovie(string tagName, string movieName);
        void DeleteTag(string tagName, string movieName);
        void DeleteMovie(string movieName);
        int? GetWatchLaterListId();
        int? GetFavoriteListId();
        bool IsMovieInList(string movieTitle, int listId);
    }
}
