using ECommerce.Contracts.Catalogs;
using WebAPP.Abstractions.Http;

namespace WebAPP.HttpClients;

public interface IECommerceHttpClient
{
    Task<HttpResponse<Responses.Catalogs>> GetCatalogsAsync(int limit, int offset, CancellationToken cancellationToken);
    Task<HttpResponse> CreateCatalogAsync(Requests.CreateCatalog request, CancellationToken cancellationToken);
    Task<HttpResponse> DeleteCatalogAsync(Guid catalogId, CancellationToken cancellationToken);
    Task<HttpResponse> EditCatalogAsync(Requests.CreateCatalog request, CancellationToken cancellationToken);
}