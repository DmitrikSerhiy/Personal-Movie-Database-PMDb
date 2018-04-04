using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services.ServicesAbstraction
{
   public interface ISearchService
    {
        bool IsExist();
        void Serialize(string MovieString);
        void MapToModel();
        MovieModel GetMovie();
        bool Validate();
    }
}
