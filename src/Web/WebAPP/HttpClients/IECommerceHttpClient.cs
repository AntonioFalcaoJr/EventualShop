using Contracts.Services.Catalogs;
using WebAPP.Abstractions.Http;

namespace WebAPP.HttpClients;

public interface IECommerceHttpClient
{
    Task<HttpResponse<PagedResult<Projection.CatalogItem>>> GetAllItemsAsync(int limit, int offset, CancellationToken cancellationToken);
    Task<HttpResponse<PagedResult<Projection.Catalog>>> GetAsync(int limit, int offset, CancellationToken cancellationToken);
    Task<HttpResponse> CreateAsync(Request.CreateCatalog request, CancellationToken cancellationToken);
    Task<HttpResponse> DeleteAsync(Guid catalogId, CancellationToken cancellationToken);
    Task<HttpResponse> ActivateAsync(Guid catalogId, Request.ChangeCatalogDescription request, CancellationToken cancellationToken);
    Task<HttpResponse> DeactivateAsync(Guid catalogId, Request.ChangeCatalogTitle request, CancellationToken cancellationToken);
    Task<HttpResponse> ChangeDescriptionAsync(Guid catalogId, Request.ChangeCatalogDescription request, CancellationToken cancellationToken);
    Task<HttpResponse> ChangeTitleAsync(Guid catalogId, Request.ChangeCatalogTitle request, CancellationToken cancellationToken);
}