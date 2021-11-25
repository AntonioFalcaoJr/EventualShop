namespace Application.Abstractions.Http;

public class HttpResponse<T>
{
    public bool Success { get; init; }
    public T ActionResult { get; init; }
}
