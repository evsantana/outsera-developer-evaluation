namespace OutseraMovies.Infra.Data.Interfaces
{
    public interface ICsvReaderService<T>
    {
        /// <summary>
        /// Reads the CSV file
        /// </summary>
        /// <param name="filePath">Path of file</param>
        /// <param name="delimiter">Delimiter character</param>
        /// <returns></returns>
        IAsyncEnumerable<T> ReadAsync(string filePath, string delimiter);

    }
}
