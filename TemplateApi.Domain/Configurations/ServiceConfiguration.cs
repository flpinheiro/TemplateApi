using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Domain.Services;

namespace TemplateApi.Domain.Configurations
{
    public static class ServiceConfiguration
    {
        public static void AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
