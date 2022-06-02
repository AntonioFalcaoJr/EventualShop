using System.Net.Http.Json;
using BlazorStrap;

namespace WebAPP.Abstractions.Http;

public abstract class ApplicationHttpClient
{
    private readonly HttpClient _client;
    private readonly IBlazorStrap _blazorStrap;

    protected ApplicationHttpClient(HttpClient client, IBlazorStrap blazorStrap)
    {
        _client = client;
        _blazorStrap = blazorStrap;
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

    // TODO - Improve this
    private async Task<HttpResponse<TProjection>> RequestAsync<TProjection>(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestAsync, CancellationToken cancellationToken)
    {
        var response = await requestAsync(_client, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return new()
            {
                Success = true,
                Message = response.ReasonPhrase,
                ActionResult = await response.Content.ReadFromJsonAsync<TProjection>(cancellationToken: cancellationToken)
            };
        }

        _blazorStrap.Toaster.Add("Error", response.ReasonPhrase, o =>
        {
            o.Color = BSColor.Danger;
            o.CloseAfter = 2000;
            o.Toast = Toast.BottomRight;
        });

        return new()
        {
            Success = false,
            Message = response.ReasonPhrase
        };
    }

    // TODO - Improve this
    private async Task<HttpResponse> RequestAsync(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestAsync, CancellationToken cancellationToken)
    {
        var response = await requestAsync(_client, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return new()
            {
                Success = true,
                Message = response.ReasonPhrase,
            };
        }

        _blazorStrap.Toaster.Add("Error", response.ReasonPhrase, o =>
        {
            o.Color = BSColor.Danger;
            o.CloseAfter = 2000;
            o.Toast = Toast.BottomRight;
        });

        return new()
        {
            Success = false,
            Message = response.ReasonPhrase
        };
    }
}