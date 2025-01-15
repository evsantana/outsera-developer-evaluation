using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutseraMovies.Domain.Entities;
using OutseraMovies.Infra.Data.Context;
using OutseraMovies.Infra.Data.Interfaces;
using OutseraMovies.Infra.Data.Services;

namespace OutseraMovies.Infra.IoC.ModuleInitializers
{
    public static class InfrastructureModuleInitializer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Database config
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")
            ));

            //Register CsvImportService dependencies
            services.AddScoped<ICsvReaderService<Movie>, CsvReaderService<Movie>>();
            services.AddScoped<CsvImportService>();


            //CSV configuration after services are resolved
            using var scope = services.BuildServiceProvider().CreateScope();
            var csvImportService = scope.ServiceProvider.GetRequiredService<CsvImportService>();

            //CSV settings
            var csvFilePath = configuration["CsvFileSettings:FilePath"];
            var csvDelimiter = configuration["CsvFileSettings:Delimiter"] ?? ";";


            //CSV imports
            if (!string.IsNullOrEmpty(csvFilePath))
            {
                csvImportService.ImportFromCsv(csvFilePath, csvDelimiter).GetAwaiter().GetResult();
            }


            return services;
        }
    }
}
