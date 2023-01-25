using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;

namespace Contracts.Services.Warehouse;

public static class Query
{
    public record ListInventoryGridItems(Paging Paging) : IQuery
    {
        public static implicit operator ListInventoryGridItems(Protobuf.ListInventoryGridItemsRequest request)
            => new(request.Paging);

    }

    public record ListInventoryItemsListItems(Guid InventoryId, Paging Paging) : IQuery
    {
        public static implicit operator ListInventoryItemsListItems(Protobuf.ListInventoryItemsListItemsRequest request)
            => new(new(request.InventoryId), request.Paging);
    }
}