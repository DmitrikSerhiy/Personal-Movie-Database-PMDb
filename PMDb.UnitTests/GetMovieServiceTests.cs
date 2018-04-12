using Autofac.Extras.Moq;
using FluentAssertions;
using Moq;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Services;
using PMDb.Services.Mappers;
using PMDb.Services.Models;
using System;
using Xunit;

namespace PMDb.UnitTests
{
    public class GetMovieServiceTests
    {
        private MovieModel fakeMovieModel;
        private MapperInitializatior mapperInitializator;

        public GetMovieServiceTests()
        {
            mapperInitializator = new MapperInitializatior();
            fakeMovieModel = new MovieModel
            {
                Title = "Oh, God!",
            };
        }


        [Fact]
        public void GetExistingMovie()
        {
            //using (var mockMovieService = AutoMock.GetLoose())
            //{
            //    mockMovieService.Mock<IMovieService>().Setup(x => x.GetMovie(13))
            //        .Returns(fakeMovieModel);

            //    mockMovieService.Provide<IMovieService, MovieService>();
            //    var sut = mockMovieService.Create<MovieService>();

            //    var actual = sut.GetMovie(13);

            //    actual.Title.Should().Be(fakeMovieModel.Title);
            //}
            //mockMovieRepository.Setup(mr => mr.GetMovie(It.IsInRange<int>(0, int.MaxValue, Range.Inclusive)));

                //var actual = mockMovieRepository.Object.GetMovie(13);

                //mockMovieRepository.Verify(mm => mm.GetMovie(13), Times.Once);
                //actual.Should().NotBeNull();
        }

        [Fact]
        public void GetNonexistingMovie()
        {
            //mockMovieRepository.Setup(mr => mr.GetMovie(It.IsInRange<int>(int.MinValue, -1, Range.Inclusive)))
            //    .Returns<Movie>(null);

            //var actual = mockMovieRepository.Object.GetMovie(-1);

            //actual.Should().BeNull();
        }

    }
}