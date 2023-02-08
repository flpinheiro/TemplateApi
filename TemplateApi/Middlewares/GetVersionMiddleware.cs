namespace TemplateApi.Middlewares;

public class GetVersionMiddleware
{
    private readonly ILogger<GetVersionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GetVersionMiddleware(RequestDelegate next, ILogger<GetVersionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var version = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
        if (version is not null)
        {
            _logger.LogDebug("application version", version);
            context.Response.Headers.Add("Version", version);
        }
        await _next(context);
    }
}

