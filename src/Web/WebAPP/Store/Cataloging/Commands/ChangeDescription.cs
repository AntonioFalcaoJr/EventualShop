using Fluxor;
using Refit;

namespace WebAPP.Store.Cataloging.Commands;

public interface IChangeDescriptionApi
{
    [Put("/v1/catalogs/{catalogId}/description")]
    Task<IApiResponse> ChangeDescriptionAsync(string catalogId, [Body] string description, CancellationToken cancellationToken);
}

public record ChangeDescription
{
    public required string CatalogId;
    public required string NewDescription;
    public required CancellationToken CancellationToken;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, ChangeDescription _)
        => state with { IsEditingDescription = false };
}

public class ChangeDescriptionEffect(IChangeDescriptionApi api) : Effect<ChangeDescription>
{
    public override async Task HandleAsync(ChangeDescription cmd, IDispatcher dispatcher)
    {
        var response = await api.ChangeDescriptionAsync(cmd.CatalogId, cmd.NewDescription, cmd.CancellationToken);

        dispatcher.Dispatch(response.IsSuccessStatusCode
            ? new EventsV1.CatalogDescriptionChanged(cmd.CatalogId, cmd.NewDescription)
            : new EventsV1.CatalogDescriptionChangeFailed(cmd.CatalogId, response.ReasonPhrase ?? response.Error.Message));
    }
}