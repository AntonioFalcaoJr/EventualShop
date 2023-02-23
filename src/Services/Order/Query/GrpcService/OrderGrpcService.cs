using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Order;
using Contracts.Services.Order.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class OrderGrpcService : OrderService.OrderServiceBase
{
    private readonly IInteractor<Query.GetOrderDetails, Projection.OrderDetails> _getOrderDetailsInteractor;
    private readonly IPagedInteractor<Query.ListOrdersGridItems, Projection.OrderGridItem> _listOrdersGridItemsInteractor;

    public OrderGrpcService(
        IInteractor<Query.GetOrderDetails, Projection.OrderDetails> getOrderDetailsInteractor,
        IPagedInteractor<Query.ListOrdersGridItems, Projection.OrderGridItem> listOrdersGridItemsInteractor)
    {
        _getOrderDetailsInteractor = getOrderDetailsInteractor;
        _listOrdersGridItemsInteractor = listOrdersGridItemsInteractor;
    }

    public override async Task<GetResponse> GetOrderDetails(GetOrderDetailsRequest request, ServerCallContext context)
    {
        var itemDetails = await _getOrderDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return itemDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((OrderDetails)itemDetails) };
    }

    public override async Task<ListResponse> ListOrdersGridItems(ListOrdersGridItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listOrdersGridItemsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((OrderGridItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}