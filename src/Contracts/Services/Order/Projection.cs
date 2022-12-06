using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Order;

public static class Projection
{
    public record OrderDetails(Guid Id, Guid CustomerId, decimal Total, Dto.Address BillingAddress, Dto.Address ShippingAddress, IEnumerable<Dto.OrderItem> Items,
        IEnumerable<Dto.PaymentMethod> PaymentMethods, string Status, bool IsDeleted) : IProjection;
}