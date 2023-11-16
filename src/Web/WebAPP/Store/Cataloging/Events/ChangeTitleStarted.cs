using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record ChangeTitleStarted
{
    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, ChangeTitleStarted _)
        => state with { IsEditingTitle = true };
}