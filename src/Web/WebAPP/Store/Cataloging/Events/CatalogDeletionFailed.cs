using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogDeletionFailed(string Error);

public class CatalogDeletionFailedReducer : Reducer<CatalogingState, CatalogDeletionFailed>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogDeletionFailed @event)
        => state with { IsDeleting = false, Error = @event.Error };
}