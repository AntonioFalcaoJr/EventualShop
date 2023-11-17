using Fluxor;
using Refit;
using WebAPP.Store.Cataloging.Events;

namespace WebAPP.Store.Cataloging.Commands;

public interface IChangeTitleApi
{
    [Put("/v1/catalogs/{catalogId}/title")]
    Task<IApiResponse> ChangeTitleAsync(string catalogId, [Body] string title, CancellationToken cancellationToken);
}

public record ChangeTitle
{
    public required string CatalogId;
    public required string NewTitle;
    public required CancellationToken CancellationToken;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, ChangeTitle _)
        => state with { IsEditingTitle = false };
}

public class ChangeTitleEffect(IChangeTitleApi api) : Effect<ChangeTitle>
{
    public override async Task HandleAsync(ChangeTitle cmd, IDispatcher dispatcher)
    {
        var response = await api.ChangeTitleAsync(cmd.CatalogId, cmd.NewTitle, cmd.CancellationToken);

        dispatcher.Dispatch(response.IsSuccessStatusCode
            ? new CatalogTitleChanged { CatalogId = cmd.CatalogId, NewTitle = cmd.NewTitle }
            : new CatalogTitleChangeFailed { CatalogId = cmd.CatalogId, Error = response.ReasonPhrase ?? response.Error.Message });
    }
}