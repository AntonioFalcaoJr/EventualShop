using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Projection
{
    public record ShoppingCartDetails(Guid Id, Guid CustomerId, string Total, string Status, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.ShoppingCartDetails(ShoppingCartDetails cart)
            => new()
            {
                Id = cart.Id.ToString(),
                CustomerId = cart.CustomerId.ToString(),
                Status = cart.Status,
                Total = cart.Total
            };
    }

    public record ShoppingCartItemDetails(Guid Id, Guid CartId, Dto.Product Product, int Quantity, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.ShoppingCartItemDetails(ShoppingCartItemDetails item)
            => new()
            {
                Id = item.Id.ToString(),
                CartId = item.CartId.ToString(),
                Product = item.Product,
                Quantity = item.Quantity
            };
    }

    public record PaymentMethodDetails(Guid Id, Guid CartId, string Amount, Dto.IPaymentOption Option, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.PaymentMethodDetails(PaymentMethodDetails method)
            => new()
            {
                Id = method.Id.ToString(),
                CartId = method.CartId.ToString(),
                Amount = method.Amount,
                Option = method.Option switch
                {
                    Dto.CreditCard creditCard => new() { CreditCard = creditCard },
                    Dto.DebitCard debitCard => new() { DebitCard = debitCard },
                    Dto.PayPal payPal => new() { PayPal = payPal },
                    _ => default
                }
            };
    }

    public record ShoppingCartItemListItem(Guid Id, Guid CartId, string ProductName, int Quantity, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.ShoppingCartItemListItem(ShoppingCartItemListItem item)
            => new()
            {
                Id = item.Id.ToString(),
                CartId = item.CartId.ToString(),
                ProductName = item.ProductName,
                Quantity = item.Quantity
            };
    }

    public record PaymentMethodListItem(Guid Id, Guid CartId, string Amount, string Option, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.PaymentMethodListItem(PaymentMethodListItem method)
            => new()
            {
                Id = method.Id.ToString(),
                CartId = method.CartId.ToString(),
                Amount = method.Amount,
                Option = method.Option
            };
    }
}