using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public static class RatingMapper
    {
        public static RatingModel Map(Rating Rating)
        {
            return Mapper.Map<RatingModel>((Rating));
        }
    }
}
