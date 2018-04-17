using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMDb.Services
{
    public class MarkValidator : AbstractValidator<double>
    {
        public MarkValidator()
        {
            RuleFor(m => m).GreaterThan(0).LessThan(10.1);
        }
    }
}
