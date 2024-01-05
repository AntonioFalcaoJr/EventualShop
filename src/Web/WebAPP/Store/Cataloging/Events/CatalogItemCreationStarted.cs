using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogItemCreationStarted(string CatalogId);

public class CatalogItemCreationStartedReducer : Reducer<CatalogingState, CatalogItemCreationStarted>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogItemCreationStarted @event)
        => state with
        {
            CatalogId = @event.CatalogId,
            IsCreatingItem = true,
            NewItem = new(),
            Error = string.Empty
        };
}