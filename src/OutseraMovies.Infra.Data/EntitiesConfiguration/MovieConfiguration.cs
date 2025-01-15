using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutseraMovies.Domain.Entities;

namespace OutseraMovies.Infra.Data.EntitiesConfiguration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        /// <summary>
        /// Settings for the Movie table
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            //Primary key
            builder.HasKey(x => x.Id);
                
        }
    }
}
