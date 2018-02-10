using FluentAssertions;
using PMDb.Domain.Core;
using PMDb.Services;
using System;
using System.Text;
using Xunit;

namespace PmDb.UnitTests
{
    [Trait("Category", "Mapper Tests")]
    public class MovieMapperTests
    {
        private Movie MovieExaple;
        public MovieMapperTests()
        {
            MovieExaple = new Movie()
            {
                Id = 13,
                Name = "Divine Trash",
                Mark = 9.7,
                Genre = "Documentary",
                Director = "Stephannie Kinforth",
            };
        }

        [Fact]
        public void MovieMappingTest()
        {
            var result = MovieMapper.Map(MovieExaple);

            result.Name.Should().Be(MovieExaple.Name);
            result.Genre.Should().Be(MovieExaple.Genre);
            result.Mark.Should().Be(MovieExaple.Mark);
            result.Director.Should().Be(MovieExaple.Director);
        }
    }
}