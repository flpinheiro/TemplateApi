using Serilog;
using System.Text.Json.Serialization;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.Domain.Configurations;
using TemplateApi.Infra.Configurations;
using TemplateApi.Middlewares;

[assembly: System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var version = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version;

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    #region Configure Service
    // Add services to the container.
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }); ;
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocument(x =>
    {
        x.GenerateEnumMappingDescription = true;
        x.Title = "Template Api";
        x.PostProcess = document =>
        {
            document.Info.Version = "v1";
            document.Info.Title = "Template API";
            document.Info.Description = $"A simple ASP.NET Core web API Template for personal study and examples.\n Version: {version}";

            document.Info.Contact = new NSwag.OpenApiContact
            {
                Name = "Felipe Lu�s Pinheiro",
                Email = "flpinheiro@gmail.com",
            };
        };
    });
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddInfraConfiguration(builder.Configuration);
    builder.Services.AddServiceConfiguration();

    builder.Services.AddHttpClient();

    builder.Services.Configure<HostOptions>(hostOptions =>
    {
        hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
    });
    #endregion

    #region Configure Pipeline
    var app = builder.Build();

    app.UseSerilogRequestLogging();

    //configure global Exception Handler middleware
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    app.UseMiddleware<GetVersionMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwaggerUi3();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run(); 
    #endregion

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}