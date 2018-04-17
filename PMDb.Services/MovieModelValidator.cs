using FluentValidation;
using PMDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public class MovieModelValidator : AbstractValidator<MovieModel>
    {
        public MovieModelValidator()
        {
            RuleSet("Mark", () =>
            {
                RuleFor(m => m.Ratings.Mark).SetValidator(new MarkValidator());//does not work
            });

            RuleSet("Review", () =>
            {
                RuleFor(m => m.Review.Length).GreaterThan(0).LessThan(400);
                //some further validators for Review
            });
        }
    }
}
