using Microsoft.Data.SqlClient;
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
            var dbPassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
            var dbDataSource = Environment.GetEnvironmentVariable("DATABASE_DATASOURCE");
            var dbUserId = Environment.GetEnvironmentVariable("DATABASE_USERID");
            var dbInitialCatalog = Environment.GetEnvironmentVariable("DATABASE_INITIALCATALOG");

            
            var builder = new SqlConnectionStringBuilder();
            //builder.UserID = dbUserId ??configuration.GetConnectionString("UserId");
            builder.Password  = dbPassword ?? configuration.GetConnectionString("Password");
            builder.DataSource = dbDataSource?? configuration.GetConnectionString("DataSource");
            //builder.Encrypt = true;
            //builder.InitialCatalog = dbInitialCatalog?? configuration.GetConnectionString("InitialCatalog");
            builder.IntegratedSecurity = true;
            var connectionString = builder.ConnectionString;
            services.AddDbContext<TemplateApiContext>(context =>
            {
                context.UseSqlServer(connectionString);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
