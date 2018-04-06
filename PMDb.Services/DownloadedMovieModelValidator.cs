using FluentValidation;
using PMDb.Domain.Core;
using PMDb.Services.Models;
using System;

namespace PMDb.Services
{
    public class DownloadedMovieModelValidator : AbstractValidator<DownloadedMovieModel>
    {
        public DownloadedMovieModelValidator()
        {
            RuleSet("IMDbRatings", () =>
            {
                RuleFor(i => i.imdbRating).NotEqual("N/A");
                RuleFor(i => i.imdbVotes).NotEqual("N/A");
            });

            
            //RuleSet("Ratings", () =>
            //{
            //    RuleForEach(r => r.Ratings).Must(array => array.Count != 0);

            //});

            //RuleFor(m => m.Actors).NotEqual("N/A");
            //RuleFor(m => m.Director).NotEqual("N/A");
            //RuleFor(m => m.Genre).NotEqual("N/A");
            //RuleFor(m => m.Writer).NotEqual("N/A");
            //RuleFor(m => m.Plot).NotEqual("N/A").WithName("strings");
            //RuleFor(m => m.Year).NotEqual("N/A").WithName("strings");
            //RuleFor(m => m.Runtime).NotEqual("N/A").WithName("strings");
        }
    }
}