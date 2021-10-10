using System;
using Messages.Abstractions.Commands;

namespace Messages.ShoppingCarts
{
    public static class Commands
    {
        public record AddCartItem(Guid ProductId, string ProductName, decimal UnitPrice, Guid CartId, int Quantity) : Command;

        public record AddCreditCard(Guid CartId, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : Command;

        public record CreateCart(Guid CustomerId) : Command;

        public record CheckoutCart(Guid CartId, string BillingAddressCity, string BillingAddressCountry,
            int? BillingAddressNumber, string BillingAddressState, string BillingAddressStreet, string BillingAddressZipCode,
            string ShippingAddressCity, string ShippingAddressCountry, int? ShippingAddressNumber, string ShippingAddressState,
            string ShippingAddressStreet, string ShippingAddressZipCode) : Command;

        public record RemoveCartItem(Guid CartId, Guid ProductId) : Command;
    }
}