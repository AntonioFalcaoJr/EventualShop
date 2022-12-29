using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;
using Contracts.Services.Order.Protobuf;

namespace Contracts.Services.Order;

public static class Query
{
    public record struct GetOrderDetails(Guid OrderId) : IQuery
    {
        public static implicit operator GetOrderDetails(GetOrderDetailsRequest request)
            => new(new(request.OrderId));
    }

    public record struct ListOrdersGridItems(Guid CustomerId, Paging Paging) : IQuery
    {
        public static implicit operator ListOrdersGridItems(ListOrdersGridItemsRequest request)
            => new(new(request.CustomerId), request.Paging);
    }
}