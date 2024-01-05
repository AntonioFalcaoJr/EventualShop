﻿using Fluxor;
using Refit;
using WebAPP.Store.Cataloging.Events;

namespace WebAPP.Store.Cataloging.Commands;

public record DeleteCatalog(string CatalogId, CancellationToken CancellationToken);

public class DeleteCatalogReducer : Reducer<CatalogingState, DeleteCatalog>
{
    public override CatalogingState Reduce(CatalogingState state, DeleteCatalog action)
        => state with { IsDeleting = true };
}

public interface IDeleteCatalogApi
{
    [Delete("/v1/catalogs/{catalogId}")]
    Task<IApiResponse> DeleteAsync(string catalogId, CancellationToken cancellationToken);
}

public class DeleteCatalogEffect(IDeleteCatalogApi api) : Effect<DeleteCatalog>
{
    public override async Task HandleAsync(DeleteCatalog cmd, IDispatcher dispatcher)
    {
        var response = await api.DeleteAsync(cmd.CatalogId, cmd.CancellationToken);

        dispatcher.Dispatch(response.IsSuccessStatusCode
            ? new CatalogDeleted(cmd.CatalogId)
            : new CatalogDeletionFailed(response.Error?.Message ?? response.ReasonPhrase ?? "Unknown error"));
    }
}