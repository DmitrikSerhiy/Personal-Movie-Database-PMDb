using AutoMapper;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.Mappers
{
    public static class WriterMapper
    {
        public static List<WriterModel> Map(List<MovieWriter> Writers)
        {
            return Mapper.Map<MovieWriter[], List<WriterModel>>(Writers.ToArray());
        }
        public static List<MovieWriter> Map(List<WriterModel> Writers)
        {
            return Mapper.Map<WriterModel[], List<MovieWriter>>(Writers.ToArray());
        }
    }
}
