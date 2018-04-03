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

            movieModel.GenreModels = GenreMapper.Map(movie.MovieGenre);
            movieModel.ActorModels = ActorMapper.Map(movie.MovieActor);
            movieModel.DirectorModels = DirectorMapper.Map(movie.MovieDirector);
            movieModel.TagModels = TagMapper.Map(movie.MovieTag);
            movieModel.WriterModels = WriterMapper.Map(movie.MovieWriter);
            movieModel.Ratings = RatingMapper.Map(movie.Rating);

            return movieModel;
        }
    }
}