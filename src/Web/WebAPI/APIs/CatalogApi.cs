using System.Diagnostics.CodeAnalysis;
using Contracts.DataTransferObjects;
using Contracts.Services.Catalog;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.Validations;

namespace WebAPI.APIs;

public static class CatalogApi
{
    public static void MapCatalogApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", (IBus bus, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetCatalogs, Projection.Catalog>(bus, new(limit ?? 0, offset ?? 0), ct));

        group.MapPost("/", (IBus bus, CreateCatalogRequest request, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.CreateCatalog>(bus, new(request.CatalogId, request.Title, request.Description), ct));

        group.MapGet("/{catalogId:guid}", (IBus bus, [NotEmpty] Guid catalogId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetCatalog, Projection.Catalog>(bus, new(catalogId), ct));

        group.MapDelete("/{catalogId:guid}", (IBus bus, [NotEmpty] Guid catalogId, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.DeleteCatalog>(bus, new(catalogId), ct));

        group.MapPut("/{catalogId:guid}/activate", (IBus bus, [NotEmpty] Guid catalogId, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.ActivateCatalog>(bus, new(catalogId), ct));

        group.MapPut("/{catalogId:guid}deactivate", (IBus bus, [NotEmpty] Guid catalogId, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.DeactivateCatalog>(bus, new(catalogId), ct));

        group.MapPut("/{catalogId:guid}/description", (IBus bus, [NotEmpty] Guid catalogId, [NotNull] string description, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.ChangeCatalogDescription>(bus, new(catalogId, description), ct));

        group.MapPut("/{catalogId:guid}/title", (IBus bus, [NotEmpty] Guid catalogId, [NotNull] string title, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.ChangeCatalogTitle>(bus, new(catalogId, title), ct));

        group.MapGet("/items", (IBus bus, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetAllItems, Projection.CatalogItem>(bus, new(limit ?? 0, offset ?? 0), ct));

        group.MapGet("/{catalogId:guid}/items", (IBus bus, Guid catalogId, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetCatalogItems, Projection.CatalogItem>(bus, new(catalogId, limit ?? 0, offset ?? 0), ct));

        group.MapPost("/{catalogId:guid}/items", (IBus bus, Guid catalogId, AddCatalogItemRequest request, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.AddCatalogItem>(bus, new(catalogId, request.InventoryId, request.Product, request.UnitPrice, request.Sku, request.Quantity), ct));

        group.MapDelete("/{catalogId:guid}/items/{itemId:guid}", (IBus bus, [NotEmpty] Guid catalogId, [NotEmpty] Guid itemId, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.DeleteCatalogItem>(bus, new(catalogId, itemId), ct));

        group.WithMetadata(new TagsAttribute("Catalogs"));
    }

    private record struct CreateCatalogRequest(Guid CatalogId, string Title, string Description);

    private record struct AddCatalogItemRequest(Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity);
}