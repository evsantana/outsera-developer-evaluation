using Moq;
using OutseraMovies.Application.Services;
using OutseraMovies.Domain.Entities;
using OutseraMovies.Domain.Interfaces;

namespace OutseraMovies.Tests
{
    [TestFixture]
    public class MovieServiceTests
    {
        private Mock<IMovieRepository> _mockRepository;
        private MovieService _service;

        [SetUp]
        public void Setup()
        {
            //Configure repository mock
            _mockRepository = new Mock<IMovieRepository>();

            _mockRepository.Setup(repo => repo.GetWinnersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Movie>
                {
                new Movie(2008, "Movie 1", "Studio 1", true, "Producer 1 and Producer 2"),
                new Movie(2009, "Movie 2", "Studio 2", true, "Producer 1"),
                new Movie(2018, "Movie 3", "Studio 3", true, "Producer 2"),
                new Movie(2019, "Movie 4", "Studio 4", true, "Producer 2"),
                new Movie(1900, "Movie 5", "Studio 5", true, "Producer 1"),
                new Movie(1999, "Movie 6", "Studio 6", true, "Producer 1"),
                new Movie(2007, "Movie 7", "Studio 7", false, "Producer 3")
                });

            //Create the service with the mocked repository
            _service = new MovieService(_mockRepository.Object);
        }

        [Test]
        public async Task GetProducerIntervalMinMax_ShouldReturnExpectedResults()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _service.GetProducerIntervalMinMax(cancellationToken);

            // Assert
            Assert.IsNotNull(result, "O resultado não deveria ser nulo.");
            Assert.IsNotEmpty(result.Min, "A lista de intervalos mínimos não deveria estar vazia.");
            Assert.IsNotEmpty(result.Max, "A lista de intervalos máximos não deveria estar vazia.");

            //Check min intervals
            Assert.IsTrue(result.Min.Any(r =>
                r.Producer == "Producer 2" 
                && r.Interval == 1 
                && r.PreviousWin == 2018 
                && r.FollowingWin == 2019),
                "O intervalo mínimo esperado para 'Producer 2' não foi encontrado.");

            Assert.IsTrue(result.Min.Any(r =>
                r.Producer == "Producer 1" 
                && r.Interval == 1 
                && r.PreviousWin == 2008 
                && r.FollowingWin == 2009),
                "O intervalo mínimo esperado para 'Producer 1' não foi encontrado.");

            //Check max intervals
            Assert.IsTrue(result.Max.Any(r =>
                r.Producer == "Producer 1" 
                && r.Interval == 99 
                && r.PreviousWin == 1900 
                && r.FollowingWin == 1999),
                "O intervalo máximo esperado para 'Producer 1' não foi encontrado.");
        }
    }

}
