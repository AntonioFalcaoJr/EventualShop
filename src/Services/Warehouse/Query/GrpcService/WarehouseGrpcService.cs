using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Warehouse;
using Contracts.Services.Warehouse.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class WarehouseGrpcService : WarehouseService.WarehouseServiceBase
{
    private readonly IPagedInteractor<Query.ListInventoryItemsListItems, Projection.InventoryItemListItem> _listInventoryGridItemsInteractor;
    private readonly IPagedInteractor<Query.ListInventoryGridItems, Projection.InventoryGridItem> _listInventoriesItemsCardsInteractor;

    public WarehouseGrpcService(
        IPagedInteractor<Query.ListInventoryItemsListItems, Projection.InventoryItemListItem> listInventoryGridItemsInteractor,
        IPagedInteractor<Query.ListInventoryGridItems, Projection.InventoryGridItem> listInventoriesItemsCardsInteractor)
    {
        _listInventoryGridItemsInteractor = listInventoryGridItemsInteractor;
        _listInventoriesItemsCardsInteractor = listInventoriesItemsCardsInteractor;
    }

    public override async Task<ListResponse> ListInventoryItems(ListInventoryItemsListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listInventoryGridItemsInteractor.InteractAsync(request, context.CancellationToken);

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
        var pagedResult = await _listInventoriesItemsCardsInteractor.InteractAsync(request, context.CancellationToken);

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