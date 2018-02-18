using FluentValidation;
using PMDb.Domain.Core;
using System;

namespace PMDb.Services
{
    public class MovieValidation : AbstractValidator<Movie>
    {
        public MovieValidation()
        {
            RuleFor(m => m.Id).NotNull();
        }
    }
}