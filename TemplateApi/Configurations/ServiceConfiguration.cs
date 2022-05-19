using TemplateApi.Domain.Interfaces.Services;
using TemplateApi.Services;

namespace TemplateApi.Configurations
{
    public static class ServiceConfiguration
    {
        public static void AddServiceConfiguration(this IServiceCollection services) 
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
