using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Projection
{
    public record ShoppingCart(Guid Id, Customer Customer, string Status, decimal Total = default, bool IsDeleted = default) : IProjection;

    public record ShoppingCartItem(Guid CartId, Guid Id, Dto.Product Product, int Quantity, bool IsDeleted) : IProjection;

    public record Customer(Guid Id, Dto.Address ShippingAddress = default, Dto.Address BillingAddress = default);

    public record CreditCard(Guid Id, Guid CartId, decimal Amount, Dto.CreditCard Method, bool IsDeleted) : IPaymentMethod;

    public record DebitCard(Guid Id, Guid CartId, decimal Amount, Dto.DebitCard Method, bool IsDeleted) : IPaymentMethod;

    public record PayPal(Guid Id, Guid CartId, decimal Amount, Dto.PayPal Method, bool IsDeleted) : IPaymentMethod;

    public interface IPaymentMethod : IProjection
    {
        Guid CartId { get; }
    }
}