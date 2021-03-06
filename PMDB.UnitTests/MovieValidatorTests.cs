﻿using FluentAssertions;
using FluentValidation.Results;
using PMDb.Domain.Core;
using PMDb.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PMDb.UnitTests
{
    [Trait("Category", "Movie Validation tests")]
    public class MovieValidatorTests
    {
        public MovieValidatorTests()
        {

        }

        [Fact]
        public void WrongMovieIdValidationTest()
        {
            //var negativeMovieId = new Movie() { Id = -1 };
            //DownloadedMovieModelValidator validator = new DownloadedMovieModelValidator();

            //ValidationResult results = validator.Validate(negativeMovieId);

            //results.IsValid.Should().BeFalse();
        }

    //    [Fact]
    //    public void LongMovieNameValidationTest()
    //    {
    //        var longMoviename = new Movie()
    //        {
    //            Name = "too long movie name for example:" +
    //" Three Billboards Outside Ebbing, Missouri"
    //        };
    //        MovieValidation validator = new MovieValidation();

    //        ValidationResult results = validator.Validate(longMoviename);

    //        results.IsValid.Should().BeFalse();
    //    }

    //    [Theory]
    //    [InlineData(-1)]
    //    [InlineData(11)]
    //    public void InvalidMovieMarkValidationTest(int Mark)
    //    {
    //        var invalidMovieMark = new Movie() { Mark = Mark };
    //        MovieValidation validator = new MovieValidation();

    //        ValidationResult results = validator.Validate(invalidMovieMark);

    //        results.IsValid.Should().BeFalse();
    //    }

    //    [Fact]
    //    public void LongMovieDirectorNameValidationTest()
    //    {
    //        var LongMovieDirectorName = new Movie()
    //        {
    //            Director = "This is example of too long director's name even longer than you could emagine"
    //        };
    //        MovieValidation validator = new MovieValidation();

    //        ValidationResult results = validator.Validate(LongMovieDirectorName);

    //        results.IsValid.Should().BeFalse();
    //    }

    //    [Fact]
    //    public void LongMovieGenreValidationTest()
    //    {
    //        var LongMovieGenre = new Movie()
    //        {
    //            Director = "This is example of too long movie's genre even longer than you could emagine"
    //        };
    //        MovieValidation validator = new MovieValidation();

    //        ValidationResult results = validator.Validate(LongMovieGenre);

    //        results.IsValid.Should().BeFalse();
    //    }
    }
}