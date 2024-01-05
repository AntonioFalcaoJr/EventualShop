using Fluxor;
using Microsoft.AspNetCore.Components;
using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogCreated(Catalog NewCatalog);

public class CatalogCreatedReducer : Reducer<CatalogingState, CatalogCreated>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogCreated @event)
    {
        var catalogs = state.Catalogs.Insert(0, @event.NewCatalog);

        Page page = new() { HasNext = catalogs.Count > state.Page.Size };
        if (page.HasNext) catalogs = catalogs.RemoveAt(catalogs.Count - 1);

        return state with
        {
            IsCreating = false,
            NewCatalog = new(),
            Catalogs = catalogs,
            Page = page
        };
    }
}

public class CatalogCreatedEffect(NavigationManager manager) : Effect<CatalogCreated>
{
    public override Task HandleAsync(CatalogCreated @event, IDispatcher dispatcher)
    {
        manager.NavigateTo($"/catalogs/{@event.NewCatalog.CatalogId}");
        return Task.CompletedTask;
    }
}