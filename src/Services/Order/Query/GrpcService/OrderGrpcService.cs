using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Order;
using Contracts.Services.Order.Protobuf;
using Grpc.Core;

namespace GrpcService;

public class OrderGrpcService : OrderService.OrderServiceBase
{
    private readonly IInteractor<Query.GetOrderDetails, Projection.OrderDetails> _getOrderDetailsInteractor;
    private readonly IInteractor<Query.ListOrdersGridItems, IPagedResult<Projection.OrderGridItem>> _listOrdersGridItemsInteractor;

    public OrderGrpcService(
        IInteractor<Query.GetOrderDetails, Projection.OrderDetails> getOrderDetailsInteractor,
        IInteractor<Query.ListOrdersGridItems, IPagedResult<Projection.OrderGridItem>> listOrdersGridItemsInteractor)
    {
        _getOrderDetailsInteractor = getOrderDetailsInteractor;
        _listOrdersGridItemsInteractor = listOrdersGridItemsInteractor;
    }

    public override async Task<OrderDetailsResponse> GetOrderDetails(GetOrderDetailsRequest request, ServerCallContext context)
    {
        var orderDetails = await _getOrderDetailsInteractor.InteractAsync(request, context.CancellationToken);
        return orderDetails is null ? new() { NotFound = new() } : new() { OrderDetails = orderDetails };
    }

    public override async Task<OrdersGridItemsPagedResponse> ListOrdersGridItems(ListOrdersGridItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listOrdersGridItemsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult!.Items.Any()
            ? new()
            {
                OrderGridItems =
                {
                    Items = { pagedResult.Items.Select(gridItem => (OrderGridItem)gridItem) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}