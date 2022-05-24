using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TemplateApi.CrossCutting.Constants;
using TemplateApi.CrossCutting.Exceptions;
using TemplateApi.CrossCutting.Models;

namespace TemplateApi.CrossCutting.Middlewares;

public class GlobalExceptionHandlerMIddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMIddleware> _logger;

    public GlobalExceptionHandlerMIddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMIddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = ContentTypeConstants.JsonType;
        var response = context.Response;

        var errorResponse = new ErrorResponse(exception);
        switch (exception)
        {
            case ApplicationException ex:
                if (ex.Message.Contains("Invalid token"))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                }
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case KeyNotFoundException:
            case PersonNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal Server errors. Check Logs!";
                break;
        }
        _logger.LogError(exception.Message);
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}

