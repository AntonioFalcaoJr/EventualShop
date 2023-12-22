using Fluxor;
using Refit;
using WebAPP.Store.Cataloging.Events;
using WebAPP.Store.Catalogs;

namespace WebAPP.Store.Cataloging.Commands;

public interface IAddCatalogItemApi
{
    [Post("/v1/catalogs/{catalogId}/items")]
    Task<IApiResponse> AddCatalogItemAsync(string catalogId, [Body] CatalogItem item, CancellationToken cancellationToken);
}

public record AddCatalogItem
{
    public required string CatalogId;
    public required CatalogItem NewItem;
    public required CancellationToken CancellationToken;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, AddCatalogItem _)
        => state with { IsAddingItem = true };
}

public class AddCatalogItemEffect(IAddCatalogItemApi api) : Effect<AddCatalogItem>
{
    public override async Task HandleAsync(AddCatalogItem cmd, IDispatcher dispatcher)
    {
        var response = await api.AddCatalogItemAsync(cmd.CatalogId, cmd.NewItem, cmd.CancellationToken);

        dispatcher.Dispatch(response.IsSuccessStatusCode
            ? new CatalogItemAdded { NewItem = cmd.NewItem }
            : new CatalogItemAddingFailed { Error = response.ReasonPhrase ?? response.Error.Message });
    }
}