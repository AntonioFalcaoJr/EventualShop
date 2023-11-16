using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogCreationFailed
{
    public required string Error;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, CatalogCreationFailed @event)
        => state with { IsCreating = false, Error = @event.Error, NewCatalog = new() };
}