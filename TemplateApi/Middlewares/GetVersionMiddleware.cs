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
        var version = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version;
        if (version !=  null)
        {
            var IbVersion = string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            _logger.LogDebug($"application version: {IbVersion}", version);
            context.Response.Headers.Add("Version", IbVersion); 
        }
        await _next(context);
    }
}

