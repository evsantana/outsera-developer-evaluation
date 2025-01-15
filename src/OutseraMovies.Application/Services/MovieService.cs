using OutseraMovies.Application.DTOs;
using OutseraMovies.Application.Interfaces;
using OutseraMovies.Application.Mappings;
using OutseraMovies.Domain.Interfaces;

namespace OutseraMovies.Application.Services
{
    public class MovieService : IMovieService
    {

        #region Properties and Constructors
        private IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        #endregion


        public async Task<MovieDTO?> CreateAsync(MovieDTO entity, CancellationToken cancellationToken)
        {
            var response = await _movieRepository.CreateAsync(MovieMapping.toEntity(entity), cancellationToken);

            return MovieMapping.toDTO(response);
        }

        public async Task DeleteAsync(MovieDTO entity, CancellationToken cancellationToken)
        {
            var deleted = await _movieRepository.DeleteAsync(MovieMapping.toEntity(entity), cancellationToken);

            if (deleted is null)
                throw new Exception($"Entity could not be deleted");
        }

        public async Task<IEnumerable<MovieDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _movieRepository.GetAllAsync(cancellationToken);

            return MovieMapping.toListDTO(entities);
        }

        public async Task<MovieDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _movieRepository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                throw new Exception($"Entity could not be loaded");

            return MovieMapping.toDTO(entity);
        }

        public async Task UpdateAsync(MovieDTO entity, CancellationToken cancellationToken)
        {
            var producer = await _movieRepository.UpdateAsync(MovieMapping.toEntity(entity), cancellationToken);

            if (producer is null)
                throw new Exception($"Entity could not be loaded");
        }

        public async Task<MinMaxProducerResponse> GetProducerIntervalMinMax(CancellationToken cancellationToken)
        {
            var winners = await _movieRepository.GetWinnersAsync(cancellationToken);

            // Obter os produtores com os anos de premiação
            var producerYears = winners

                .SelectMany(m => m.Producers!
                    .Replace(" and", ",")
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => new { Producer = p.Trim(), m.Year }))

                .GroupBy(p => p.Producer)
                .Where(g => g.Count() > 1) // Apenas produtores com mais de um prêmio
                .SelectMany(g =>
                {
                    var years = g.Select(x => x.Year).OrderBy(y => y).ToList();

                    return years.Zip(years.Skip(1), (prev, next) =>
                        new ProducerResponse(g.Key, next - prev, prev, next));
                });

            // Determinar os intervalos mínimo e máximo
            var minInterval = producerYears.MinBy(r => r.Interval);
            var maxInterval = producerYears.MaxBy(r => r.Interval);

            // Retornar a resposta com os resultados
            return new MinMaxProducerResponse
            {
                Min = producerYears.Where(r => r.Interval == minInterval.Interval),
                Max = producerYears.Where(r => r.Interval == maxInterval.Interval)
            };
        }
    }
}
