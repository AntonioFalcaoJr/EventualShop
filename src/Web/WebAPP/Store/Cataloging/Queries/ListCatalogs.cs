using Fluxor;
using Refit;
using WebAPP.Abstractions;
using WebAPP.Store.Cataloging.Events;

namespace WebAPP.Store.Cataloging.Queries;

public interface IListCatalogsApi
{
    [Get("/v1/catalogs")]
    Task<IApiResponse<IPagedResult<Catalog>>> ListAsync([Query] Paging paging, CancellationToken cancellationToken);
}

public record ListCatalogs
{
    public required Paging Paging;
    public required CancellationToken CancellationToken;
}

public class ListCatalogsEffect(IListCatalogsApi api) : Effect<ListCatalogs>
{
    public override async Task HandleAsync(ListCatalogs query, IDispatcher dispatcher)
    {
        var response = await api.ListAsync(query.Paging, query.CancellationToken);

        dispatcher.Dispatch(response is { IsSuccessStatusCode: true, Content: not null }
            ? new CatalogsListed { Catalogs = response.Content }
            : new CatalogsListingFailed { Error = response.ReasonPhrase ?? response.Error?.Message ?? "Unknown error" });
    }
}