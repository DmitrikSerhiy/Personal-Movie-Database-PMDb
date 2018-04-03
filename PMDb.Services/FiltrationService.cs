using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services.Helpers;
using PMDb.Services.Mappers;
using PMDb.Services.Models;

namespace PMDb.Services
{
    public class FiltrationService : IFiltrationService
    {
        private IList<Dictionary<string, bool>> filters;
        private IFiltrationRepository filtrationRepository;
        private IUrlHelper urlHelper;
        private IList<Movie> filtredMovie;

        public FiltrationService(IFiltrationRepository FiltrationRepository,
            IUrlHelper UrlHelper)
        {
            urlHelper = UrlHelper;
            filtrationRepository = FiltrationRepository;
            filters = new List<Dictionary<string, bool>>();
            filtredMovie = new List<Movie>();
        }

        public void Filter(MovieFilters movieFilters)
        {
            filtrationRepository.Filter(movieFilters); 
        }

        public IList<SimplifiedMovieModel> GetFiltredMovies(PaginationParameters paginationParameters)
        {
            var filtredMmoviesBeforePagination = filtrationRepository.GetMovies();

            var pagedMovies = PagedList<Movie>.Create(filtredMmoviesBeforePagination,
                paginationParameters.PageNumber,
                paginationParameters.PageSize);


            var PagedSimplifiedFiltredMovies = new PagedList<SimplifiedMovieModel>(
                pagedMovies.CurrentPage, pagedMovies.TotalPages, pagedMovies.PageSize,
                pagedMovies.TotalCount, pagedMovies.HasPrevious, pagedMovies.HasNext);

            foreach (var item in pagedMovies)
            {
                PagedSimplifiedFiltredMovies.Add(SimplifiedMovieMapper.Map(item));
            }

            return PagedSimplifiedFiltredMovies;
        }

        public string GenerateNextPageLink(bool nextPage, PaginationParameters getMoviesParameters)
        {
            return nextPage ?
               UriProvider.CreateMoviesUri(getMoviesParameters,
               UriType.NextPage, urlHelper as UrlHelper) : null;
        }

        public string GeneratePreviousPageLink(bool previousPage, PaginationParameters getMoviesParameters)
        {
            return previousPage ?
               UriProvider.CreateMoviesUri(getMoviesParameters,
               UriType.PreviousPage, urlHelper as UrlHelper) : null;
        }
    }
}
