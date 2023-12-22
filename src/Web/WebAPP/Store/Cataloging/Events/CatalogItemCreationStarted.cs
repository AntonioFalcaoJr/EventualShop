using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogItemCreationStarted
{
    public required string CatalogId;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, CatalogItemCreationStarted @event)
        => state with
        {
            CatalogId = @event.CatalogId,
            IsCreatingItem = true,
            NewItem = new(),
            Error = string.Empty
        };
}