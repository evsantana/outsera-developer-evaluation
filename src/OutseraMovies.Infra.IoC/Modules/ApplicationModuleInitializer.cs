using Microsoft.Extensions.DependencyInjection;
using OutseraMovies.Application.Interfaces;
using OutseraMovies.Application.Services;
using OutseraMovies.Domain.Interfaces;
using OutseraMovies.Infra.Data.Repositories;

namespace OutseraMovies.Infra.IoC.ModuleInitializers
{
    public static class ApplicationModuleInitializer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            return services;
        }
    }
}
