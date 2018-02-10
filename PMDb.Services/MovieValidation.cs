﻿using FluentValidation;
using PMDb.Domain.Core;
using System;

namespace PMDb.Services
{
    public class MovieValidation : AbstractValidator<Movie>
    {
        public MovieValidation()
        {
            RuleFor(m => m.Id).NotNull();
            RuleFor(m => m.Mark).GreaterThan(0).LessThan(10).NotEmpty();
            RuleFor(m => m.Name).MaximumLength(30);
            RuleFor(m => m.Genre).MaximumLength(10);//IsInEnum()
            RuleFor(m => m.Director).MaximumLength(10);
        }
    }
}