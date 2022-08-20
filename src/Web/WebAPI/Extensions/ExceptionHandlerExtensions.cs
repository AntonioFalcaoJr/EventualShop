#nullable enable
using System.Text.Json.Serialization;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebAPI.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseApplicationExceptionHandler(this IApplicationBuilder app)
        => app.UseExceptionHandler(builder
            => builder.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                Log.Error("[Exception Handler] {Error}", exception?.Message);

                context.Response.StatusCode = GetStatusCode(exception);
                if (exception is ValidationException validationException)
                    await WriteValidationErrorAsync(context, validationException);
                else
                    await WriteProblemsAsync(context, exception);
            }));

    private static Task WriteProblemsAsync(HttpContext context, Exception? exception)
    {
        var problemDetails = new ProblemDetails();
        FillProblemDetails(context, exception, problemDetails);
        return WriteAsync(context, problemDetails);
    }

    private static Task WriteValidationErrorAsync(HttpContext context, ValidationException validationException)
    {
        var httpValidation = new HttpValidationProblemDetails(validationException.Errors
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(x => x.Key, v => v.Select(x => x).ToArray()))
        {
            Detail = "Validation failed."
        };
            
        FillProblemDetails(context, validationException, httpValidation);
        return WriteAsync(context, httpValidation);
    }

    private static Task WriteAsync<T>(HttpContext context, T response) 
        => context.Response.WriteAsJsonAsync(
            value: response,
            options: new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, },
            cancellationToken: context.RequestAborted);

    private static void FillProblemDetails(HttpContext context, Exception? exception, ProblemDetails problemDetails)
    {
        problemDetails.Detail ??= exception?.Message;
        problemDetails.Status = context.Response.StatusCode;
        problemDetails.Type = exception?.GetType().Name;
        problemDetails.Instance = context.Request.Path;
    }
    
    private static int GetStatusCode(Exception? exception) 
        => exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            TimeoutException or RequestTimeoutException => StatusCodes.Status408RequestTimeout,
            _ => StatusCodes.Status500InternalServerError
        };
}