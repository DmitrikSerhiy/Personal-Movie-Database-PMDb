﻿using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;

namespace PMDb.Services.Mappers
{
    public class MapperInitializatior
    {
        public MapperInitializatior()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Movie, MovieModel>();
                cfg.CreateMap<Movie, SimplifiedMovieModel>()
                    .ForMember(m => m.Mark, model => model.MapFrom(m => m.Rating.OwnRating));
                cfg.CreateMap<MovieGenre, GenreModel>()
                    .ForMember(mg => mg.Name, model => model.MapFrom(g => g.Genre.Name));
                cfg.CreateMap<MovieActor, ActorModel>()
                    .ForMember(ma => ma.Name, model => model.MapFrom(a => a.Actor.Name));
                cfg.CreateMap<MovieDirector, DirectorModel>()
                    .ForMember(md => md.Name, model => model.MapFrom(d => d.Director.Name));
                cfg.CreateMap<MovieTag, TagModel>()
                    .ForMember(mt => mt.Name, model => model.MapFrom(t => t.Tag.Name));
                cfg.CreateMap<MovieWriter, WriterModel>()
                    .ForMember(mw => mw.Name, model => model.MapFrom(w => w.Writer.Name));
                cfg.CreateMap<Rating, RatingModel>()
                    .ForMember(rm => rm.Mark, model => model.MapFrom(r => r.OwnRating));
                cfg.CreateMap<RatingModel, Rating>()
                    .ForMember(r => r.OwnRating, model => model.MapFrom(rm => rm.Mark));

                cfg.CreateMap<string, double>().ConvertUsing(s => Convert.ToDouble(s));
                cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));

                cfg.CreateMap<string, ActorModel>()
                    .ForMember(a => a.Name, field => field.MapFrom(scr => scr));
                cfg.CreateMap<string, DirectorModel>()
                    .ForMember(a => a.Name, field => field.MapFrom(scr => scr));
                cfg.CreateMap<string, GenreModel>()
                    .ForMember(a => a.Name, field => field.MapFrom(scr => scr));
                cfg.CreateMap<string, WriterModel>()
                    .ForMember(a => a.Name, field => field.MapFrom(scr => scr));

                cfg.CreateMap<DownloadedMovieModel, MovieModel>()
                    .ForMember(mm => mm.Year, model => model.MapFrom(y => y.Year))
                    .ForPath(mm => mm.Ratings.IMDbRating, model => model.MapFrom(r => r.imdbRating))
                    .ForPath(mm => mm.Ratings.IMDbVotes, model => model.MapFrom(r => r.imdbVotes));
            });
        }
    }
}
