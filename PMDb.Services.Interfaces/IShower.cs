using PMDb.Domain.Core;
using System;

namespace PMDb.Services.Interfaces
{
    public interface IShower
    {
        void ShowMovieMetaData(Movie movie);
    }
}
