using CsvHelper.Configuration;

namespace OutseraMovies.Infra.Data.Mappings
{
    public class MovieMap : ClassMap<MovieMapDTO>
    {
        public MovieMap()
        {
            Map(m => m.Year).Name("year");
            Map(m => m.Studios).Name("studios");
            Map(m => m.Producers).Name("producers");
            Map(m => m.Winner).Name("winner");
            Map(m => m.Title).Name("title");
        }


    }
}
