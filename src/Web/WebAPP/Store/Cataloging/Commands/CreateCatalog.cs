using Fluxor;
using Refit;
using WebAPP.Store.Cataloging.Events;

namespace WebAPP.Store.Cataloging.Commands;

public interface ICreateCatalogApi
{
    [Post("/v1/catalogs")]
    Task<IApiResponse<string>> CreateAsync([Body] Catalog catalog, CancellationToken cancellationToken);
}

public record CreateCatalog
{
    public required Catalog NewCatalog;
    public required CancellationToken CancellationToken;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, CreateCatalog _)
        => state with { IsCreating = true };
}

public class CreateCatalogEffect(ICreateCatalogApi api) : Effect<CreateCatalog>
{
    public override async Task HandleAsync(CreateCatalog cmd, IDispatcher dispatcher)
    {
        var response = await api.CreateAsync(cmd.NewCatalog, cmd.CancellationToken);

        dispatcher.Dispatch(response is { IsSuccessStatusCode: true, Content: not null }
            ? new CatalogCreated { NewCatalog = cmd.NewCatalog with { Id = response.Content } }
            : new CatalogCreationFailed { Error = response.ReasonPhrase ?? response.Error?.Message ?? "Unknown error" });
    }
}