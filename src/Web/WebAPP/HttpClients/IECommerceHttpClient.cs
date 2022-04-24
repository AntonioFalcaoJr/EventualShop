using ECommerce.Contracts.Catalogs;
using WebAPP.Abstractions.Http;

namespace WebAPP.HttpClients;

public interface IECommerceHttpClient
{
    Task<HttpResponse<PagedResult<Projection.Catalog>>> GetAsync(int limit, int offset, CancellationToken cancellationToken);
    Task<HttpResponse> CreateAsync(Requests.CreateCatalog request, CancellationToken cancellationToken);
    Task<HttpResponse> DeleteAsync(Guid catalogId, CancellationToken cancellationToken);
    Task<HttpResponse> ActivateAsync(Guid catalogId, Requests.ChangeCatalogDescription request, CancellationToken cancellationToken);
    Task<HttpResponse> DeactivateAsync(Guid catalogId, Requests.ChangeCatalogTitle request, CancellationToken cancellationToken);
    Task<HttpResponse> ChangeDescriptionAsync(Guid catalogId, Requests.ChangeCatalogDescription request, CancellationToken cancellationToken);
    Task<HttpResponse> ChangeTitleAsync(Guid catalogId, Requests.ChangeCatalogTitle request, CancellationToken cancellationToken);
}