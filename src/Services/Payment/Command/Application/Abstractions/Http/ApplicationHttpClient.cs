using System.Net.Http.Json;

namespace Application.Abstractions.Http;

public abstract class ApplicationHttpClient(HttpClient client)
{
    protected Task<HttpResponse<TResponse>> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken)
        where TResponse : new()
        => RequestAsync<TResponse>((client, ct) => client.GetAsync(endpoint, ct), cancellationToken);

    protected Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken)
        where TResponse : new()
        => RequestAsync<TResponse>((client, ct) => client.PostAsJsonAsync(endpoint, request, ct), cancellationToken);

    protected Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken)
        where TResponse : new()
        => RequestAsync<TResponse>((client, ct) => client.PutAsJsonAsync(endpoint, request, ct), cancellationToken);

    private async Task<HttpResponse<TResponse>> RequestAsync<TResponse>(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> request, CancellationToken cancellationToken)
        where TResponse : new()
    {
        var response = await request(client, cancellationToken);

        return new()
        {
            Success = response.IsSuccessStatusCode,
            ActionResult = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ?? new()
                : new()
        };
    }
}