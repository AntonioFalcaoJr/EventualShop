using Contracts.Services.Warehouse.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Warehouses.Validators;

namespace WebAPI.APIs.Warehouses;

public static class Queries
{
    public record ListInventoryGridItems(WarehouseService.WarehouseServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListInventoryGridItemsValidator>, IQuery<WarehouseService.WarehouseServiceClient>
    {
        public static implicit operator ListInventoryGridItemsRequest(ListInventoryGridItems request)
            => new() { Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListInventoryItemsListItems(WarehouseService.WarehouseServiceClient Client, Guid InventoryId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListInventoryGridItemsValidator>, IQuery<WarehouseService.WarehouseServiceClient>
    {
        public static implicit operator ListInventoryItemsListItemsRequest(ListInventoryItemsListItems request)
            => new() { InventoryId = request.InventoryId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}