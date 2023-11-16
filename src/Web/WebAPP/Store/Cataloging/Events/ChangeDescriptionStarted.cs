using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record ChangeDescriptionStarted
{
    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, ChangeDescriptionStarted _)
        => state with { IsEditingDescription = true };
}