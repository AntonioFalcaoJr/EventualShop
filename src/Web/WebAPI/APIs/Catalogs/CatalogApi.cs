using Contracts.Services.Catalog;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.Validations;

namespace WebAPI.APIs.Catalogs;

public static class CatalogApi
{
    public static void MapCatalogApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", (IBus bus, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetCatalogs, Projection.Catalog>(bus, new(limit ?? 0, offset ?? 0), ct));

        group.MapPost("/", ([AsParameters] Requests.CreateCatalog request)
            => ApplicationApi.SendCommandAsync<Command.CreateCatalog>(request));

        group.MapGet("/{catalogId:guid}", (IBus bus, [NotEmpty] Guid catalogId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetCatalog, Projection.Catalog>(bus, new(catalogId), ct));

        group.MapDelete("/{catalogId:guid}", ([AsParameters] Requests.DeleteCatalog request)
            => ApplicationApi.SendCommandAsync<Command.DeleteCatalog>(request));

        group.MapPut("/{catalogId:guid}/activate", ([AsParameters] Requests.ActivateCatalog request)
            => ApplicationApi.SendCommandAsync<Command.ActivateCatalog>(request));

        group.MapPut("/{catalogId:guid}deactivate", ([AsParameters] Requests.DeactivateCatalog request)
            => ApplicationApi.SendCommandAsync<Command.DeactivateCatalog>(request));

        group.MapPut("/{catalogId:guid}/description", ([AsParameters] Requests.ChangeCatalogDescription request)
            => ApplicationApi.SendCommandAsync<Command.ChangeCatalogDescription>(request));

        group.MapPut("/{catalogId:guid}/title", ([AsParameters] Requests.ChangeCatalogTitle request)
            => ApplicationApi.SendCommandAsync<Command.ChangeCatalogTitle>(request));

        group.MapGet("/items", (IBus bus, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetAllItems, Projection.CatalogItem>(bus, new(limit ?? 0, offset ?? 0), ct));

        group.MapGet("/{catalogId:guid}/items", (IBus bus, Guid catalogId, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetCatalogItems, Projection.CatalogItem>(bus, new(catalogId, limit ?? 0, offset ?? 0), ct));

        group.MapPost("/{catalogId:guid}/items", ([AsParameters] Requests.AddCatalogItem request)
            => ApplicationApi.SendCommandAsync<Command.AddCatalogItem>(request));

        group.MapDelete("/{catalogId:guid}/items/{itemId:guid}", ([AsParameters] Requests.RemoveCatalogItem request)
            => ApplicationApi.SendCommandAsync<Command.RemoveCatalogItem>(request));

        group.WithMetadata(new TagsAttribute("Catalogs"));
    }
}