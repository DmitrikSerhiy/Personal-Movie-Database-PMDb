using FluentAssertions;
using Moq;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using System;
using System.Linq.Expressions;
using Xunit;

namespace PmDb.UnitTests
{
    public class GetMovieTests
    {
        private Movie fakeMovie;
        private Mock<IMovieRepository> mockMovieRepository;

        public GetMovieTests()
        {
            mockMovieRepository = new Mock<IMovieRepository>();
            fakeMovie = new Movie()
            {
                Id = 13,
                Name = "Divine Trash",
                Mark = 9.7,
                Genre = "Documentary",
                Director = "Stephannie Kinforth",
            };
        }


        [Fact]
        public void GetExistingMovie()
        {
            mockMovieRepository.Setup(mr => mr.GetMovie(It.IsInRange<int>(0, int.MaxValue, Range.Inclusive)))
                .Returns(fakeMovie);

            var actual = mockMovieRepository.Object.GetMovie(13);

            actual.Should().Be(fakeMovie);
        }

        [Fact]
        public void GetNonexistingMovie()
        {
            mockMovieRepository.Setup(mr => mr.GetMovie(It.IsInRange<int>(int.MinValue, -1, Range.Inclusive)))
                .Returns<Movie>(null);

            var actual = mockMovieRepository.Object.GetMovie(-1);

            actual.Should().BeNull();
        }

    }
}