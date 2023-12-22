using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogItemAddingFailed
{
    public required string Error;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, CatalogItemAddingFailed @event)
        => state with { IsAddingItem = false, Error = @event.Error };
}