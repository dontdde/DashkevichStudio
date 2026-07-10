using DashkevichStudio.Application.Leads;
using Microsoft.AspNetCore.Mvc;

namespace DashkevichStudio.Api.Middleware;

public sealed class ApiExceptionMiddleware(
    RequestDelegate next,
    ILogger<ApiExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (LeadValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Проверьте данные формы.",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { ["errors"] = exception.Errors }
            });
        }
        catch (BadHttpRequestException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Не удалось прочитать форму.",
                Detail = exception.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled API error. TraceId: {TraceId}", context.TraceIdentifier);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Не удалось отправить заявку.",
                Detail = "Попробуйте ещё раз или свяжитесь с нами напрямую.",
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { ["traceId"] = context.TraceIdentifier }
            });
        }
    }
}
