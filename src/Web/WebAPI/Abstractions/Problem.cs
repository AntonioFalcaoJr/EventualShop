namespace WebAPI.Abstractions;

public sealed class Problem : IResult
{
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Task.CompletedTask;
    }
}