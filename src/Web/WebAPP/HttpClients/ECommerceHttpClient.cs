using ECommerce.Contracts.Catalogs;
using Microsoft.Extensions.Options;
using WebAPP.Abstractions.Http;
using WebAPP.DependencyInjection.Options;

namespace WebAPP.HttpClients;

public class ECommerceHttpClient : ApplicationHttpClient, IECommerceHttpClient
{
    private readonly ECommerceHttpClientOptions _options;

    public ECommerceHttpClient(HttpClient client, IOptionsSnapshot<ECommerceHttpClientOptions> optionsSnapshot)
        : base(client)
    {
        _options = optionsSnapshot.Value;
    }

    public Task<HttpResponse<Responses.Catalogs>> GetAsync(int limit, int offset, CancellationToken cancellationToken)
        => GetAsync<Responses.Catalogs>($"{_options.CatalogEndpoint}?limit={limit}&offset={offset}", cancellationToken);

    public Task<HttpResponse> CreateAsync(Requests.CreateCatalog request, CancellationToken cancellationToken)
        => PostAsync($"{_options.CatalogEndpoint}", request, cancellationToken);

    public Task<HttpResponse> DeleteAsync(Guid catalogId, CancellationToken cancellationToken)
        => DeleteAsync($"{_options.CatalogEndpoint}/{catalogId}", cancellationToken);

    public Task<HttpResponse> ActivateAsync(Guid catalogId, Requests.ChangeCatalogDescription request, CancellationToken cancellationToken)
        => PutAsync($"{_options.CatalogEndpoint}/{catalogId}", request, cancellationToken);

    public Task<HttpResponse> DeactivateAsync(Guid catalogId, Requests.ChangeCatalogTitle request, CancellationToken cancellationToken)
        => PutAsync($"{_options.CatalogEndpoint}/{catalogId}", request, cancellationToken);

    public Task<HttpResponse> ChangeDescriptionAsync(Guid catalogId, Requests.ChangeCatalogDescription request, CancellationToken cancellationToken)
        => PutAsync($"{_options.CatalogEndpoint}/{catalogId}", request, cancellationToken);

    public Task<HttpResponse> ChangeTitleAsync(Guid catalogId, Requests.ChangeCatalogTitle request, CancellationToken cancellationToken)
        => PutAsync($"{_options.CatalogEndpoint}/{catalogId}", request, cancellationToken);
}