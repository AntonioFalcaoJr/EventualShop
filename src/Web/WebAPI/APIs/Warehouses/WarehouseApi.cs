using Asp.Versioning.Builder;
using Contracts.Services.Warehouse;
using Contracts.Services.Warehouse.Protobuf;
using MassTransit;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Warehouses;

public static class WarehouseApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/warehouses/";

    public static IVersionedEndpointRouteBuilder MapWarehouseApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/", ([AsParameters] Requests.ListInventoryGridItems request)
            => ApplicationApi.ListAsync<WarehouseService.WarehouseServiceClient, InventoryGridItem>
                (request, (client, ct) => client.ListInventoryGridItemsAsync(request, cancellationToken: ct)));

        group.MapPost("/", ([AsParameters] Requests.CreateInventory request)
            => ApplicationApi.SendCommandAsync<Command.CreateInventory>(request));

        // group.MapGet("/{inventoryId:guid}/items", (IBus bus, Guid inventoryId, ushort? limit, ushort? offset, CancellationToken cancellationToken)
        //     => ApplicationApi.GetPagedProjectionAsync<Query.ListInventoryItems, Projection.InventoryItem>(bus, new(inventoryId, limit ?? 0, offset ?? 0), cancellationToken));

        group.MapPost("/{inventoryId:guid}/items", ([AsParameters] Requests.ReceiveInventoryItem request)
            => ApplicationApi.SendCommandAsync<Command.ReceiveInventoryItem>(request));

        group.MapPut("/{inventoryId:guid}/items/{itemId:guid}/increase-adjust", ([AsParameters] Requests.IncreaseInventoryAdjust request)
            => ApplicationApi.SendCommandAsync<Command.IncreaseInventoryAdjust>(request));

        group.MapPut("/{inventoryId:guid}/items/{itemId:guid}/decrease-adjust", ([AsParameters] Requests.DecreaseInventoryAdjust request)
            => ApplicationApi.SendCommandAsync<Command.DecreaseInventoryAdjust>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapWarehouseApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/", ([AsParameters] Requests.ListInventoryGridItems request)
            => ApplicationApi.ListAsync<WarehouseService.WarehouseServiceClient, InventoryGridItem>
                (request, (client, ct) => client.ListInventoryGridItemsAsync(request, cancellationToken: ct)));

        return builder;
    }
}