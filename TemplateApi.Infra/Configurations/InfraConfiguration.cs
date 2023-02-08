using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TemplateApi.Domain.Interfaces.Repositories;
using TemplateApi.Infra.Context;


namespace TemplateApi.Infra.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class InfraConfiguration
    {
        public static void AddInfraConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            var connectionString = dbConnectionString ?? configuration.GetConnectionString("PersonDb");
            services.AddDbContext<TemplateApiContext>(context =>
            {
                context.UseSqlServer(connectionString);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
