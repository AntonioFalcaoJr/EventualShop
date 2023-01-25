using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class Projection
{
    public record OrderDetails(Guid Id, Guid CustomerId, Dto.Money Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.OrderItem> Items,
        IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.OrderDetails(OrderDetails order)
            => new()
            {
                OrderId = order.Id.ToString(),
                CustomerId = order.CustomerId.ToString(),
                Total = order.Total,
                Status = order.Status
            };
    }

    public record OrderGridItem(Guid Id, Guid CustomerId, Dto.Money Total, string Status, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.OrderGridItem(OrderGridItem order)
            => new()
            {
                OrderId = order.Id.ToString(),
                CustomerId = order.CustomerId.ToString(),
                Total = order.Total,
                Status = order.Status
            };
    }
}