using Asp.Versioning.Builder;
using Contracts.Services.Warehouse.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Warehouses;

public static class WarehouseApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/warehouses/";

    public static IVersionedEndpointRouteBuilder MapWarehouseApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/", ([AsParameters] Queries.ListInventoryGridItems query)
            => ApplicationApi.ListAsync<WarehouseService.WarehouseServiceClient, InventoryGridItem>
                (query, (client, ct) => client.ListInventoryGridItemsAsync(query, cancellationToken: ct)));

        group.MapPost("/", ([AsParameters] Commands.CreateInventory createInventory)
            => ApplicationApi.SendCommandAsync(createInventory));

        group.MapGet("/{inventoryId:guid}/items", ([AsParameters] Queries.ListInventoryItemsListItems query)
            => ApplicationApi.ListAsync<WarehouseService.WarehouseServiceClient, InventoryItemListItem>
                (query, (client, ct) => client.ListInventoryItemsAsync(query, cancellationToken: ct)));
        
        group.MapPost("/{inventoryId:guid}/items", ([AsParameters] Commands.ReceiveInventoryItem receiveInventoryItem)
            => ApplicationApi.SendCommandAsync(receiveInventoryItem));

        group.MapPut("/{inventoryId:guid}/items/{itemId:guid}/increase-adjust", ([AsParameters] Commands.IncreaseInventoryAdjust increaseInventoryAdjust)
            => ApplicationApi.SendCommandAsync(increaseInventoryAdjust));

        group.MapPut("/{inventoryId:guid}/items/{itemId:guid}/decrease-adjust", ([AsParameters] Commands.DecreaseInventoryAdjust decreaseInventoryAdjust)
            => ApplicationApi.SendCommandAsync(decreaseInventoryAdjust));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapWarehouseApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/", ([AsParameters] Queries.ListInventoryGridItems query)
            => ApplicationApi.ListAsync<WarehouseService.WarehouseServiceClient, InventoryGridItem>
                (query, (client, ct) => client.ListInventoryGridItemsAsync(query, cancellationToken: ct)));

        return builder;
    }
}