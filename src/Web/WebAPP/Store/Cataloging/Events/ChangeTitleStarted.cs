﻿using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record ChangeTitleStarted;

public class ChangeTitleStartedReducer : Reducer<CatalogingState, ChangeTitleStarted>
{
    public override CatalogingState Reduce(CatalogingState state, ChangeTitleStarted _)
        => state with { IsEditingTitle = true };
}