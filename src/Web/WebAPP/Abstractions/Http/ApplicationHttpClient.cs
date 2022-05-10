using System.Net.Http.Json;

namespace WebAPP.Abstractions.Http;

public abstract class ApplicationHttpClient
{
    private readonly HttpClient _client;

    protected ApplicationHttpClient(HttpClient client)
    {
        _client = client;
    }

    protected Task<HttpResponse<TProjection>> GetAsync<TProjection>(string endpoint, CancellationToken cancellationToken)
        => RequestAsync<TProjection>((client, ct) => client.GetAsync(endpoint, ct), cancellationToken);

    protected Task<HttpResponse> PostAsync<TRequest>(string endpoint, TRequest request, CancellationToken cancellationToken)
        => RequestAsync((client, ct) => client.PostAsJsonAsync(endpoint, request, ct), cancellationToken);

    protected Task<HttpResponse> PutAsync(string endpoint, CancellationToken cancellationToken)
        => RequestAsync((client, ct) => client.PutAsync(endpoint, default, ct), cancellationToken);

    protected Task<HttpResponse> PutAsync<TRequest>(string endpoint, TRequest request, CancellationToken cancellationToken)
        => RequestAsync((client, ct) => client.PutAsJsonAsync(endpoint, request, ct), cancellationToken);

    protected Task<HttpResponse> DeleteAsync(string endpoint, CancellationToken cancellationToken)
        => RequestAsync((client, ct) => client.DeleteAsync(endpoint, ct), cancellationToken);

    private async Task<HttpResponse<TProjection>> RequestAsync<TProjection>(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestAsync, CancellationToken cancellationToken)
    {
        var response = await requestAsync(_client, cancellationToken);

        return new()
        {
            Success = response.IsSuccessStatusCode,
            Message = response.ReasonPhrase,
            ActionResult = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<TProjection>(cancellationToken: cancellationToken)
                : default
        };
    }

    private async Task<HttpResponse> RequestAsync(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestAsync, CancellationToken cancellationToken)
    {
        var response = await requestAsync(_client, cancellationToken);

        return new()
        {
            Success = response.IsSuccessStatusCode,
            Message = response.ReasonPhrase
        };
    }
}