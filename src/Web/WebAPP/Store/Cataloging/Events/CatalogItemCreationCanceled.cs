using Fluxor;
using Microsoft.AspNetCore.Components;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogItemCreationCanceled;

public class CatalogItemCreationCanceledReducer : Reducer<CatalogingState, CatalogItemCreationCanceled>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogItemCreationCanceled _)
        => state with { IsAddingItem = false };
}

public class CatalogItemCreationCanceledEffect(NavigationManager manager) : Effect<CatalogItemCreationCanceled>
{
    public override Task HandleAsync(CatalogItemCreationCanceled cmd, IDispatcher dispatcher)
    {
        manager.NavigateTo("/catalogs");
        return Task.CompletedTask;
    }
}