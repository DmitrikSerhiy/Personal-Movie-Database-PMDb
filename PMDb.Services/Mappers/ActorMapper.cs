using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public static class ActorMapper
    {
        public static List<ActorModel> Map(List<MovieActor> Actors)
        {
            return Mapper.Map<MovieActor[], List<ActorModel>>(Actors.ToArray());
        }
    }
}
