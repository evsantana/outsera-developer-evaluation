using Microsoft.Extensions.Logging;
using OutseraMovies.Infra.Data.Interfaces;
using OutseraMovies.Domain.Entities;
using OutseraMovies.Domain.Interfaces;


namespace OutseraMovies.Infra.Data.Services
{
    public class CsvImportService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly ICsvReaderService<Movie> _csvReader;
        private readonly ILogger<CsvImportService> _logImport;

        public CsvImportService(IMovieRepository movieRepository,
            ICsvReaderService<Movie> csvReader,
            ILogger<CsvImportService> logImport)
        {
            _movieRepository = movieRepository;
            _csvReader = csvReader;
            _logImport = logImport;
        }

        /// <summary>
        /// Imports the CSV file
        /// </summary>
        /// <param name="filePath">Path of file</param>
        /// <param name="delimiter">Delimiter character</param>
        /// <returns></returns>
        public async Task ImportFromCsv(string filePath, string delimiter)
        {
            try
            {
                _logImport.LogInformation($"Starting CSV file import: {filePath}");

                await _movieRepository.DeleteAllAsync(); //clean database
                int count = 0;
                await foreach (var rec in _csvReader.ReadAsync(filePath, delimiter))
                {
                    try
                    {
                        await _movieRepository.CreateAsync(rec, new CancellationToken());
                        count++;
                    }
                    catch (Exception ex)
                    {
                        _logImport.LogError($"Error processing record {rec}: {ex.Message}");

                    }
                }

                _logImport.LogInformation($"CSV import completed.");
                _logImport.LogInformation($"Total imported records: {count.ToString()}");
            }
            catch (Exception ex)
            {
                _logImport.LogError($"Error importing CSV: {ex.Message}");
                //throw;
            }



        }
    }
}
