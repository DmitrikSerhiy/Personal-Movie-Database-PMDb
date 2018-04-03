using AutoMapper;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;

namespace PMDb.Services.Mappers
{
    public static class DownloadedMovieMaper
    {
        public static MovieModel Map(DownloadedMovieModel movies)
        {
            if (movies.imdbVotes.Contains(","))
                movies.imdbVotes = movies.imdbVotes.Replace(",", "");

            MovieModel Model = Mapper.Map<MovieModel>((movies));
            Model.Ratings = MapRatings(movies.Ratings, Model.Ratings.IMDbVotes);
            Model.ActorModels = MapStringsToModels<ActorModel>(movies.Actors);
            Model.DirectorModels = MapStringsToModels<DirectorModel>(movies.Director);
            Model.GenreModels = MapStringsToModels<GenreModel>(movies.Genre);
            Model.WriterModels = MapStringsToModels<WriterModel>(movies.Writer);

            return Model;
        }

        private static RatingModel MapRatings(
            Dictionary<string, string>[] dictionaries, int? votes)
        {
            RatingModel ratingModel = new RatingModel();
            foreach (var dictionary in dictionaries)
            {
                if (dictionary.ContainsValue("Internet Movie Database"))
                    ratingModel.IMDbRating = FormRatingWithSlash(dictionary["Value"]);

                if (dictionary.ContainsValue("Rotten Tomatoes"))
                    ratingModel.MetaCriticRating = FormRatingWithPersentage(dictionary["Value"]);

                if (dictionary.ContainsValue("Metacritic"))
                    ratingModel.RotenTomatosRating = FormRatingWithSlash(dictionary["Value"]);
            }
            ratingModel.IMDbVotes = votes;
            return ratingModel;
        }

        private static double FormRatingWithSlash(string rating)
        {
            int index = rating.LastIndexOf("/");
            if (index > 0) rating = rating.Substring(0, index);
            return Convert.ToDouble(rating);
        }

        private static double FormRatingWithPersentage(string rating)
        {
            return Convert.ToDouble(rating.Remove(rating.Length - 1));
        }

        private static List<T> MapStringsToModels<T>(string actors)
        {
            List<T> Models = new List<T>();
            var smth = actors.Split(", ");
            foreach (var item in smth)
            {
                Models.Add(Mapper.Map<T>(item));
            }
            return Models;
        }
    }
}
