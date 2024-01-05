using Fluxor;
using Refit;
using WebAPP.Store.Cataloging.Events;

namespace WebAPP.Store.Cataloging.Commands;

public record Identifier(string Value);

public record CreateCatalog(Catalog NewCatalog, CancellationToken CancellationToken);

public class CreateCatalogReducer : Reducer<CatalogingState, CreateCatalog>
{
    public override CatalogingState Reduce(CatalogingState state, CreateCatalog action)
        => state with { IsCreating = true };
}

public interface ICreateCatalogApi
{
    [Post("/v1/catalogs")]
    Task<IApiResponse<Identifier>> CreateAsync([Body] Catalog catalog, CancellationToken cancellationToken);
}

public class CreateCatalogEffect(ICreateCatalogApi api) : Effect<CreateCatalog>
{
    public override async Task HandleAsync(CreateCatalog cmd, IDispatcher dispatcher)
    {
        var response = await api.CreateAsync(cmd.NewCatalog, cmd.CancellationToken);

        dispatcher.Dispatch(response is { IsSuccessStatusCode: true, Content: not null }
            ? new CatalogCreated(cmd.NewCatalog with { CatalogId = response.Content.Value })
            : new CatalogCreationFailed(response.Error?.Message ?? response.ReasonPhrase ?? "Unknown error"));
    }
}