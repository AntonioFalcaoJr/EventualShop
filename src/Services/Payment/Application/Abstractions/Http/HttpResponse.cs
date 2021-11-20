namespace Application.Abstractions.Http;

public class HttpResponse<T>
{
    public bool Success { get; init; }
    public T PayloadResult { get; init; }
}
