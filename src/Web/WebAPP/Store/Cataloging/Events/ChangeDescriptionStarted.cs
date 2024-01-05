using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record ChangeDescriptionStarted;

public class ChangeDescriptionStartedReducer : Reducer<CatalogingState, ChangeDescriptionStarted>
{
    public override CatalogingState Reduce(CatalogingState state, ChangeDescriptionStarted _)
        => state with { IsEditingDescription = true };
}