using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public class MapperInitializatior
    {
        public MapperInitializatior()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Movie, MovieModel>();
                cfg.CreateMap<MovieGenre, GenreModel>()
                    .ForMember("Name", model => model.MapFrom(g => g.Genre.Name));
                cfg.CreateMap<MovieActor, ActorModel>()
                    .ForMember("Name", model => model.MapFrom(a => a.Actor.Name));
                cfg.CreateMap<MovieDirector, DirectorModel>()
                    .ForMember("Name", model => model.MapFrom(d => d.Director.Name));
                cfg.CreateMap<MovieTag, TagModel>()
                    .ForMember("Name", model => model.MapFrom(t => t.Tag.Name));
                cfg.CreateMap<MovieWriter, WriterModel>()
                    .ForMember("Name", model => model.MapFrom(w => w.Writer.Name));
                cfg.CreateMap<Rating, RatingModel>()
                    .ForMember("Mark", model => model.MapFrom(r => r.OwnRating));
                cfg.CreateMap<RatingModel, Rating>()
                    .ForMember("OwnRating", model => model.MapFrom(r => r.Mark));
            });
        }
    }
}
