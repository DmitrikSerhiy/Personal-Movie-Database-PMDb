using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public class MovieServiceValidator : AbstractValidator<MovieService>
    {
        public MovieServiceValidator()
        {
            RuleSet("Mark", () =>
            {
                RuleFor(m => m.markToAdd).GreaterThan(0).LessThan(11);
            });
        }
    }
}
