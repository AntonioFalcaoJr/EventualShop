using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogItemAddingFailed(string Error);

public class CatalogItemAddingFailedReducer : Reducer<CatalogingState, CatalogItemAddingFailed>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogItemAddingFailed @event)
        => state with { IsAddingItem = false, Error = @event.Error };
}