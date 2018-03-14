using System;
using System.Collections.Generic;
using System.Text;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Models;

namespace PMDb.Services
{
    public class FiltrationService : IFiltrationService
    {
        private IList<Dictionary<string, bool>> filters;
        private IFiltrationRepository filtrationRepository;

        public FiltrationService(IFiltrationRepository FiltrationRepository)
        {
            filtrationRepository = FiltrationRepository;
            filters = new List<Dictionary<string, bool>>();
        }
        public SimplifiedMovieModel Filter(MovieFilters movieFilters)
        {
            //map to simplifiedMovieModel
            filtrationRepository.Filter(movieFilters);

            throw new NotImplementedException();

        }
    }
}
