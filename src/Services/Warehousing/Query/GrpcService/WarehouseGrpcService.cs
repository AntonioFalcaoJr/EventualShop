using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Boundaries.Warehouse;
using Contracts.Services.Warehouse.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class WarehouseGrpcService(IPagedInteractor<Query.ListInventoryItemsListItems, Projection.InventoryItemListItem> listInventoryGridItemsInteractor,
        IPagedInteractor<Query.ListInventoryGridItems, Projection.InventoryGridItem> listInventoriesItemsCardsInteractor)
    : WarehouseService.WarehouseServiceBase
{
    public override async Task<ListResponse> ListInventoryItems(ListInventoryItemsListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await listInventoryGridItemsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((InventoryItemListItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }

    public override async Task<ListResponse> ListInventoryGridItems(ListInventoryGridItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await listInventoriesItemsCardsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((InventoryGridItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}