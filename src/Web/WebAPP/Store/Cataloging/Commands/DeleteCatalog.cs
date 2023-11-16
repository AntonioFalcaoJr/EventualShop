using Fluxor;
using Refit;
using WebAPP.Store.Cataloging.Events;

namespace WebAPP.Store.Cataloging.Commands;

public interface IDeleteCatalogApi
{
    [Delete("/v1/catalogs/{catalogId}")]
    Task<IApiResponse> DeleteAsync(string catalogId, CancellationToken cancellationToken);
}

public record DeleteCatalog
{
    public required string CatalogId;
    public required CancellationToken CancellationToken;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, DeleteCatalog _)
        => state with { IsDeleting = true };
}

public class DeleteCatalogEffect(IDeleteCatalogApi api) : Effect<DeleteCatalog>
{
    public override async Task HandleAsync(DeleteCatalog cmd, IDispatcher dispatcher)
    {
        var response = await api.DeleteAsync(cmd.CatalogId, cmd.CancellationToken);

        dispatcher.Dispatch(response.IsSuccessStatusCode
            ? new CatalogDeleted { CatalogId = cmd.CatalogId }
            : new CatalogDeletionFailed { Error = response.ReasonPhrase ?? response.Error?.Message ?? "Unknown error" });
    }
}