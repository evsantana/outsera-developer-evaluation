using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OutseraMovies.Infra.Data.Context;

namespace OutseraMovies.Infra.IoC.ModuleInitializers
{
    public static class MigrationsModuleInitializer
    {
        public static IServiceCollection ApplyMigrations(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider(); //Gera o IServiceProvider a partir do IServiceCollection.
            using var scope = serviceProvider.CreateScope(); //Cria o escopo necessário.

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            dbContext.Database.Migrate();

            return services;
        }
    }
}
