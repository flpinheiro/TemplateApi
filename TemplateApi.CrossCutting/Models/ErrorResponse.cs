namespace TemplateApi.CrossCutting.Models;

public class ErrorResponse
{
    public const bool Success = false;

    public ErrorResponse(Exception exception)
    {
        Message = exception.Message;
    }

    public string Message { get; set; }
}

