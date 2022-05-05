using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Projection
{
    public record ShoppingCart(Guid Id, Customer Customer, string Status, decimal Total = default, bool IsDeleted = default) : IProjection;

    public record ShoppingCartItem(Guid CartId, Guid Id, Dto.Product Product, int Quantity, bool IsDeleted) : IProjection;

    public record Customer(Guid Id, Dto.Address ShippingAddress, Dto.Address BillingAddress, bool IsDeleted) : Dto.Customer(ShippingAddress, BillingAddress), IProjection;

    public record PaymentMethod(Guid CartId, Guid Id, decimal Amount, Dto.IPaymentOption Option, bool IsDeleted) : Dto.PaymentMethod(Id, Amount, Option), IProjection;
}