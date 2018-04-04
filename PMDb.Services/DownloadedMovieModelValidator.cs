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
            RuleFor(m => m.Actors).NotEqual("N/A").WithName("strings");
            RuleFor(m => m.Director).NotEqual("N/A").WithName("strings");
            RuleFor(m => m.Genre).NotEqual("N/A").WithName("strings");
            RuleFor(m => m.Writer).NotEqual("N/A").WithName("strings");

            RuleFor(m => m.Plot).NotEqual("N/A").WithName("strings");
            RuleFor(m => m.Year).NotEqual("N/A").WithName("strings");
            RuleFor(m => m.Runtime).NotEqual("N/A").WithName("strings");
            RuleFor(m => m.Ratings).Must(d => d.Length != 0).WithName("list");
        }
    }
}