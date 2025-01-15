using CsvHelper;
using CsvHelper.Configuration;
using OutseraMovies.Domain.Entities;
using OutseraMovies.Infra.Data.Interfaces;
using OutseraMovies.Infra.Data.Mappings;
using System.Globalization;
using System.Text;

namespace OutseraMovies.Infra.Data.Services
{
    public class CsvReaderService<T> : ICsvReaderService<T>
    {
        /// <summary>
        /// Reads the CSV file
        /// </summary>
        /// <param name="filePath">Path of file</param>
        /// <param name="delimiter">Delimiter character</param>
        /// <returns></returns>
        public async IAsyncEnumerable<T> ReadAsync(string filePath, string delimiter)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8,
                Delimiter = delimiter,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, configuration);

            csv.Context.RegisterClassMap<MovieMap>(); //mapping csv

            var records = csv.GetRecords<MovieMapDTO>().ToList();

            foreach (var item in records)
            {
                if (typeof(T) == typeof(Movie))
                {
                    var movie = new Movie(item.Year, item.Title, item.Studios, item.Winner == "yes", item.Producers);

                    yield return (T)(object)movie;
                }
                else
                {
                    throw new InvalidOperationException($"Type '{typeof(T).Name}' is not supported for mapping.");
                }
            }
        }
    }
}
