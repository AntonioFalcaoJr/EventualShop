using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogDeletionFailed
{
    public required string Error;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, CatalogDeletionFailed @event)
        => state with { IsDeleting = false, Error = @event.Error };
}