using System.Text.Json.Serialization;
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
                var message = exception?.Message + " " + exception?.InnerException?.Message;

                Log.Error("[Exception Handler] {Error}", message);

                context.Response.StatusCode = exception switch
                {
                    BadHttpRequestException => StatusCodes.Status400BadRequest,
                    TimeoutException or RequestTimeoutException => StatusCodes.Status408RequestTimeout,
                    _ => StatusCodes.Status500InternalServerError
                };

                ProblemDetails problemDetails = new()
                {
                    Detail = message,
                    Status = context.Response.StatusCode,
                    Type = exception?.GetType().Name,
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(
                    value: problemDetails,
                    options: new() {DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull},
                    cancellationToken: context.RequestAborted);
            }));
}