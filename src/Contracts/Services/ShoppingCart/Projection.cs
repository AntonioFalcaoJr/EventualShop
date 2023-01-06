using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Projection
{
    public record ShoppingCartDetails(Guid Id, Guid CustomerId, Dto.Address? BillingAddress, Dto.Address? ShippingAddress, string Status, decimal Total, bool IsDeleted) : IProjection;

    public record ShoppingCartItemDetails(Guid Id, Guid CartId, Dto.Product Product, int Quantity, bool IsDeleted) : IProjection;

    public record PaymentMethodDetails(Guid Id, Guid CartId, decimal Amount, Dto.IPaymentOption? Option, bool IsDeleted) : IProjection;

    public record ShoppingCartItemListItem(Guid Id, Guid CartId, string Product, int Quantity, bool IsDeleted) : IProjection;

    public record PaymentMethodListItem(Guid Id, Guid CartId, string Option, bool IsDeleted) : IProjection;
}