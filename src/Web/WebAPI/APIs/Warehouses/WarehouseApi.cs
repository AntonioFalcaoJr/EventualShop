using Contracts.Services.Warehouse;
using MassTransit;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Warehouses;

public static class WarehouseApi
{
    public static RouteGroupBuilder MapWarehouseApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", (IBus bus, ushort limit, ushort offset, CancellationToken cancellationToken)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetInventories, Projection.Inventory>(bus, new(limit, offset), cancellationToken));

        group.MapPost("/", ([AsParameters] Requests.CreateInventory request)
            => ApplicationApi.SendCommandAsync<Command.CreateInventory>(request));

        group.MapGet("/{inventoryId:guid}/items", (IBus bus, Guid inventoryId, ushort? limit, ushort? offset, CancellationToken cancellationToken)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetInventoryItems, Projection.InventoryItem>(bus, new(inventoryId, limit ?? 0, offset ?? 0), cancellationToken));

        group.MapPost("/{inventoryId:guid}/items", ([AsParameters] Requests.ReceiveInventoryItem request)
            => ApplicationApi.SendCommandAsync<Command.ReceiveInventoryItem>(request));

        group.MapPut("/{inventoryId:guid}/items/{itemId:guid}/increase-adjust", ([AsParameters] Requests.IncreaseInventoryAdjust request)
            => ApplicationApi.SendCommandAsync<Command.IncreaseInventoryAdjust>(request));

        group.MapPut("/{inventoryId:guid}/items/{itemId:guid}/decrease-adjust", ([AsParameters] Requests.DecreaseInventoryAdjust request)
            => ApplicationApi.SendCommandAsync<Command.DecreaseInventoryAdjust>(request));

        return group.WithMetadata(new TagsAttribute("Warehouses"));
    }
}