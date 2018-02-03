using Autofac.Extras.Moq;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using System;
using Xunit;

namespace PmDb.MovieRepositoryTest
{
    public class GetMovie
    {
        private Movie MockTempMovie;
        public GetMovie( )
        {


            MockTempMovie = new Movie
            {
                Id = 13,
                Name = "Divine Trash",
                Mark = 9.7,
                Genre = "Documentary",
                Director = "Stephannie Kinforth"
            };
        }

        [Fact]
        public void Test1()
        {
           // var movieFromDB = _repository.GetMovie(13);

            //using (var mock = AutoMock.GetLoose())
            //{
            //    // The AutoMock class will inject a mock IDependency
            //    // into the SystemUnderTest constructor
            //    var sut = mock.Create<MovieRepository>();

            //    Assert.Equal(sut.GetMovie(13).Name, MockTempMovie.Name);
            //}

            //Assert.Equal(movieFromDB.Name, MockTempMovie.Name);
        }

        //[Fact]
        //public void Test2()
        //{
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        // The AutoMock class will inject a mock IDependency
        //        // into the SystemUnderTest constructor
        //        var sut = mock.Create<MovieRepository>();
        //    }
        //}
    }
}
