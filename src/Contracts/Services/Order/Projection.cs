using System.Globalization;
using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using MassTransit.Observables;

namespace Contracts.Services.Order;

public static class Projection
{
    public record OrderDetails(Guid Id, Guid CustomerId, decimal Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.OrderItem> Items,
        IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.OrderDetails(OrderDetails order)
            => new()
            {
                OrderId = order.Id.ToString(),
                CustomerId = order.CustomerId.ToString(),
                Total = order.Total.ToString(CultureInfo.InvariantCulture),
                Status = order.Status,
            };
    }

    public record OrderGridItem(Guid Id, Guid CustomerId, decimal Total, string Status, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.OrderGridItem(OrderGridItem order)
            => new()
            {
                OrderId = order.Id.ToString(),
                CustomerId = order.CustomerId.ToString(),
                Total = order.Total.ToString(CultureInfo.InvariantCulture),
                Status = order.Status,
            };
    }
}