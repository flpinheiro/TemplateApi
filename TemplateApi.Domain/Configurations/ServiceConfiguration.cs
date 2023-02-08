using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Interfaces.Validator;
using TemplateApi.Domain.Models.Validators;
using TemplateApi.Domain.Services;

namespace TemplateApi.Domain.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class ServiceConfiguration
    {
        public static void AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddSingleton<IPersonValidation, PersonValidation>();
        }
    }
}
