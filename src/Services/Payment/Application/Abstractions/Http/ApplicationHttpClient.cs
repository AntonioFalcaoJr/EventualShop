using System.Net.Http.Json;

namespace Application.Abstractions.Http;

public abstract class ApplicationHttpClient
{
    private readonly HttpClient _client;

    protected ApplicationHttpClient(HttpClient client) 
        => _client = client;

    protected Task<HttpResponse<TResponse>> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken)
        where TResponse : new()
        => RequestAsync<TResponse>((client, cancellationToken) => client.GetAsync(endpoint, cancellationToken), cancellationToken);

    protected Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken)
        where TResponse : new()
        => RequestAsync<TResponse>((client, cancellationToken) => client.PostAsJsonAsync(endpoint, request, cancellationToken), cancellationToken);

    protected Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken)
        where TResponse : new()
        => RequestAsync<TResponse>((client, cancellationToken) => client.PutAsJsonAsync(endpoint, request, cancellationToken), cancellationToken);

    private async Task<HttpResponse<TResponse>> RequestAsync<TResponse>(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> request, CancellationToken cancellationToken)
        where TResponse : new()
    {
        var response = await request(_client, cancellationToken);

        return new()
        {
            Success = true /* TODO - response.IsSuccessStatusCode */,
            ActionResult = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken)
                : new()
        };
    }
}