﻿using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<TemplateApiContext>(context =>
            {
                var templateString = configuration.GetConnectionString("TemplateString");
                context.UseSqlServer(templateString);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
