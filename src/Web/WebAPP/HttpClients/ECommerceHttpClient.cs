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

    public Task<HttpResponse<Responses.Catalogs>> GetCatalogsAsync(int limit, int offset, CancellationToken cancellationToken)
        => GetAsync<Responses.Catalogs>($"{_options.CatalogEndpoint}/get-catalogs?limit={limit}&offset={offset}", cancellationToken);

    public Task<HttpResponse> CreateCatalogAsync(Requests.CreateCatalog request, CancellationToken cancellationToken)
        => PostAsync($"{_options.CatalogEndpoint}/create-catalog", request, cancellationToken);

    public Task<HttpResponse> DeleteCatalogAsync(Guid catalogId, CancellationToken cancellationToken)
        => DeleteAsync($"{_options.CatalogEndpoint}/delete-catalog/{catalogId}", cancellationToken);

    public Task<HttpResponse> EditCatalogAsync(Requests.CreateCatalog request, CancellationToken cancellationToken)
        => PutAsync($"{_options.CatalogEndpoint}/update-catalog", request, cancellationToken);
}