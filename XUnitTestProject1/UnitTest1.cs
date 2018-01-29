using PMDb.Domain.Interfeces;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private IMovieRepository _repository;
        private int i = 0;
        public UnitTest1(IMovieRepository repository)
        {
            _repository = repository;
        }

        [Fact]
        public void InjectRepository()
        {
            Assert.NotEqual(0,i);
        }
    }
}
