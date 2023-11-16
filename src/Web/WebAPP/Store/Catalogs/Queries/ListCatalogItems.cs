using Fluxor;
using Refit;
using WebAPP.Abstractions;
using WebAPP.Store.Catalogs.Events;

namespace WebAPP.Store.Catalogs.Queries;

public interface IListCatalogItemsApi
{
    [Get("/v1/catalogs/{catalogId}/items")]
    Task<IApiResponse<IPagedResult<CatalogItem>>> ListItemsAsync(string catalogId, [Query] Paging paging, CancellationToken cancellationToken);
}

public record ListCatalogItems
{
    public required string CatalogId;
    public required Paging Paging;
    public required CancellationToken CancellationToken;

    [ReducerMethod]
    public static CatalogState Reduce(CatalogState state, ListCatalogItems _)
        => state with { IsFetching = true, HasError = false };
}

public class ListCatalogItemsEffect(IListCatalogItemsApi api) : Effect<ListCatalogItems>
{
    public override async Task HandleAsync(ListCatalogItems query, IDispatcher dispatcher)
    {
        var response = await api.ListItemsAsync(query.CatalogId, query.Paging, query.CancellationToken);

        dispatcher.Dispatch(response is { IsSuccessStatusCode: true, Content: not null }
            ? new CatalogItemsListed { PagedResult = response.Content }
            : new CatalogItemsListingFailed { Error = response.ReasonPhrase ?? response.Error?.Message ?? "Unknown error" });
    }
}