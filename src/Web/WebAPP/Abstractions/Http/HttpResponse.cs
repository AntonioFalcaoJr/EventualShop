namespace WebAPP.Abstractions.Http;

public class HttpResponse
{
    public bool Success { get; init; }
    public string? Message { get; init; }
}

public class HttpResponse<T> : HttpResponse
{
    public T? ActionResult { get; init; }
}