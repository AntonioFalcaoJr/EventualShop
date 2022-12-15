using Asp.Versioning.Builder;
using Contracts.Services.Catalog;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Catalogs;

public static class CatalogApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/catalogs/";

    public static IVersionedEndpointRouteBuilder MapCatalogApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("/", ([AsParameters] Requests.CreateCatalog request)
            => ApplicationApi.SendCommandAsync<Command.CreateCatalog>(request));

        group.MapGet("/grid-items", ([AsParameters] Requests.ListCatalogsGridItems request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListCatalogsGridItemsAsync(request, cancellationToken: cancellationToken)));

        group.MapDelete("/{catalogId:guid}", ([AsParameters] Requests.DeleteCatalog request)
            => ApplicationApi.SendCommandAsync<Command.DeleteCatalog>(request));

        group.MapPut("/{catalogId:guid}/activate", ([AsParameters] Requests.ActivateCatalog request)
            => ApplicationApi.SendCommandAsync<Command.ActivateCatalog>(request));

        group.MapPut("/{catalogId:guid}/deactivate", ([AsParameters] Requests.DeactivateCatalog request)
            => ApplicationApi.SendCommandAsync<Command.DeactivateCatalog>(request));

        group.MapPut("/{catalogId:guid}/description", ([AsParameters] Requests.ChangeCatalogDescription request)
            => ApplicationApi.SendCommandAsync<Command.ChangeCatalogDescription>(request));

        group.MapPut("/{catalogId:guid}/title", ([AsParameters] Requests.ChangeCatalogTitle request)
            => ApplicationApi.SendCommandAsync<Command.ChangeCatalogTitle>(request));

        group.MapGet("/{catalogId:guid}/items/list-items", ([AsParameters] Requests.ListCatalogItemsListItems request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListCatalogItemsListItemsAsync(request, cancellationToken: cancellationToken)));

        group.MapGet("/{catalogId:guid}/items/cards", ([AsParameters] Requests.ListCatalogItemsCards request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListCatalogItemsCardsAsync(request, cancellationToken: cancellationToken)));

        group.MapPost("/{catalogId:guid}/items", ([AsParameters] Requests.AddCatalogItem request)
            => ApplicationApi.SendCommandAsync<Command.AddCatalogItem>(request));

        group.MapGet("/{catalogId:guid}/items/{itemId:guid}/details", ([AsParameters] Requests.GetCatalogItemDetails request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.GetCatalogItemDetailsAsync(request, cancellationToken: cancellationToken)));

        group.MapDelete("/{catalogId:guid}/items/{itemId:guid}", ([AsParameters] Requests.RemoveCatalogItem request)
            => ApplicationApi.SendCommandAsync<Command.RemoveCatalogItem>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapCatalogApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/", ([AsParameters] Requests.ListCatalogsGridItems request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListCatalogsGridItemsAsync(request, cancellationToken: cancellationToken)));

        return builder;
    }
}