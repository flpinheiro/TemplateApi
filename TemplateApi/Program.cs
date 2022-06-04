using Serilog;
using System.Text.Json.Serialization;
using TemplateApi.CrossCutting.Middlewares;
using TemplateApi.CrossCutting.Utils;
using TemplateApi.Domain.Configurations;
using TemplateApi.Infra.Configurations;

[assembly: System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    // Add services to the container.

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        }); ;
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocument(x => x.Title = "Template Api");
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddInfraConfiguration(builder.Configuration);
    builder.Services.AddServiceConfiguration();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwaggerUi3();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<GlobalExceptionHandlerMIddleware>();

    app.MapControllers();

    app.Run();

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