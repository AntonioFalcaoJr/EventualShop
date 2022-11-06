using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Projection
{
    public record ShoppingCart(Guid Id, Guid CustomerId, Dto.Address? BillingAddress, Dto.Address? ShippingAddress, string Status, decimal Total, bool IsDeleted) : IProjection;

    public record ShoppingCartItem(Guid CartId, Guid Id, Dto.Product Product, int Quantity, bool IsDeleted) : IProjection;

    public record PaymentMethod(Guid CartId, Guid Id, decimal Amount, Dto.IPaymentOption Option, bool IsDeleted) : Dto.PaymentMethod(Id, Amount, Option), IProjection;
}