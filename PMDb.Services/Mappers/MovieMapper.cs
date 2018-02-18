using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using PMDb.Services;
using System;

namespace PMDb.Services.Mappers
{
    public static class MovieMapper
    {
        public static MovieModel Map(Movie movie)
        {
            var movieModel = Mapper.Map<MovieModel>((movie));

            movieModel.Genres = GenreMapper.Map(movie.MovieGenre);
            movieModel.Actors = ActorMapper.Map(movie.MovieActor);
            movieModel.Directors = DirectorMapper.Map(movie.MovieDirector);
            movieModel.Tags = TagMapper.Map(movie.MovieTag);
            movieModel.Writers = WriterMapper.Map(movie.MovieWriter);
            movieModel.Ratings = RatingMapper.Map(movie.Rating);

            return movieModel;
        }
    }
}