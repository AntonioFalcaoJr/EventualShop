using Contracts.Services.Catalog;
using WebAPP.Abstractions.Http;

namespace WebAPP.HttpClients;

public interface ICatalogHttpClient
{
    Task<HttpResponse<PagedResult<Projection.CatalogItemListItem>>> GetAllItemsAsync(int limit, int offset, CancellationToken cancellationToken);
    Task<HttpResponse<PagedResult<Projection.CatalogDetails>>> GetAsync(int limit, int offset, CancellationToken cancellationToken);
    Task<HttpResponse> CreateAsync(Requests.CreateCatalog request, CancellationToken cancellationToken);
    Task<HttpResponse> DeleteAsync(Guid catalogId, CancellationToken cancellationToken);
    Task<HttpResponse> ActivateAsync(Guid catalogId, CancellationToken cancellationToken);
    Task<HttpResponse> DeactivateAsync(Guid catalogId, CancellationToken cancellationToken);
    Task<HttpResponse> ChangeDescriptionAsync(Guid catalogId, string description, CancellationToken cancellationToken);
    Task<HttpResponse> ChangeTitleAsync(Guid catalogId, string title, CancellationToken cancellationToken);
    Task<HttpResponse> AddCatalogItemAsync(Guid catalogId, Requests.AddCatalogItem item, CancellationToken cancellationToken);
}