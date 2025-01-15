using OutseraMovies.Application.DTOs;
using OutseraMovies.Domain.Entities;

namespace OutseraMovies.Application.Mappings
{
    public static class MovieMapping
    {
        public static MovieDTO toDTO(Movie movie)
        {
            if (movie is null)
                return null;

            var response = new MovieDTO(
               movie.Id,
               movie.Year,
               movie.Title,
               movie.Studios,
               movie.Producers,
               movie.Winner
            ); ;

            return response;
        }

        public static Movie toEntity(MovieDTO movie)
        {
            if (movie is null)
                return null;

            var response = new Movie(
                movie.Id,
                movie.Year,
                movie.Title,
                movie.Studios,
                movie.Winner,
                movie.Producers
            );

            return response;
        }

        public static List<MovieDTO> toListDTO(IEnumerable<Movie> movies)
        {
            var list = new List<MovieDTO>();
            foreach (var item in movies)
            {
                list.Add(
                    new MovieDTO(
                        item.Id,
                        item.Year,
                        item.Title,
                        item.Studios,
                        item.Producers,
                        item.Winner
                    )                  
                );
            }

            return list;
        }
    }
}
