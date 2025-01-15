using OutseraMovies.Application.DTOs;

namespace OutseraMovies.Application.Interfaces
{
    public interface IMovieService : IBaseService<MovieDTO>
    {
        Task<MinMaxProducerResponse> GetProducerIntervalMinMax(CancellationToken cancellationToken);
    }
}
