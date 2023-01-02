using Asp.Versioning.Builder;
using Contracts.Services.Catalog;
using Contracts.Services.Catalog.Protobuf;
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
            => ApplicationApi.ListAsync<CatalogService.CatalogServiceClient, CatalogGridItem>
                (request, (client, ct) => client.ListCatalogsGridItemsAsync(request, cancellationToken: ct)));

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

        group.MapPost("/{catalogId:guid}/items", ([AsParameters] Requests.AddCatalogItem request)
            => ApplicationApi.SendCommandAsync<Command.AddCatalogItem>(request));

        group.MapGet("/{catalogId:guid}/items/list-items", ([AsParameters] Requests.ListCatalogItemsListItems request)
            => ApplicationApi.ListAsync<CatalogService.CatalogServiceClient, CatalogItemListItem>
                (request, (client, ct) => client.ListCatalogItemsListItemsAsync(request, cancellationToken: ct)));

        group.MapGet("/{catalogId:guid}/items/cards", ([AsParameters] Requests.ListCatalogItemsCards request)
            => ApplicationApi.ListAsync<CatalogService.CatalogServiceClient, CatalogItemCard>
                (request, (client, ct) => client.ListCatalogItemsCardsAsync(request, cancellationToken: ct)));

        group.MapGet("/{catalogId:guid}/items/{itemId:guid}/details", ([AsParameters] Requests.GetCatalogItemDetails request)
            => ApplicationApi.GetAsync<CatalogService.CatalogServiceClient, CatalogItemDetails>
                (request, (client, ct) => client.GetCatalogItemDetailsAsync(request, cancellationToken: ct)));

        group.MapDelete("/{catalogId:guid}/items/{itemId:guid}", ([AsParameters] Requests.RemoveCatalogItem request)
            => ApplicationApi.SendCommandAsync<Command.RemoveCatalogItem>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapCatalogApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/grid-items", ([AsParameters] Requests.ListCatalogsGridItems request)
            => ApplicationApi.ListAsync<CatalogService.CatalogServiceClient, CatalogGridItem>
                (request, (client, ct) => client.ListCatalogsGridItemsAsync(request, cancellationToken: ct)));

        return builder;
    }
}