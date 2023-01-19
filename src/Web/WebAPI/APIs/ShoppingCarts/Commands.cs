using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.ShoppingCarts.Validators;

namespace WebAPI.APIs.ShoppingCarts;

public static class Commands
{
    public record CreateCart(IBus Bus, Payloads.CreateCartPayload Payload, CancellationToken CancellationToken)
        : Validatable<CreateCartValidator>, ICommand<Command.CreateCart>
    {
        public Command.CreateCart Command => new(Payload.CustomerId, Payload.Currency);
    }

    public record AddCartItem(IBus Bus, Guid CartId, Payloads.AddCartItemPayload Payload, CancellationToken CancellationToken)
        : Validatable<AddCartItemValidator>, ICommand<Command.AddCartItem>
    {
        public Command.AddCartItem Command
            => new(CartId, Payload.CatalogId, Payload.InventoryId, Payload.Product, Payload.Quantity, Payload.UnitPrice);
    }

    public record CheckOut(IBus Bus, Guid CartId, CancellationToken CancellationToken)
        : Validatable<CheckOutValidator>, ICommand<Command.CheckOutCart>
    {
        public Command.CheckOutCart Command => new(CartId);
    }

    public record ChangeCartItemQuantity(IBus Bus, Guid CartId, Guid ItemId, ushort Quantity, CancellationToken CancellationToken)
        : Validatable<ChangeCartItemQuantityValidator>, ICommand<Command.ChangeCartItemQuantity>
    {
        public Command.ChangeCartItemQuantity Command => new(CartId, ItemId, Quantity);
    }

    public record RemoveCartItem(IBus Bus, Guid CartId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<RemoveCartItemValidator>, ICommand<Command.RemoveCartItem>
    {
        public Command.RemoveCartItem Command => new(CartId, ItemId);
    }

    public record AddShippingAddress(IBus Bus, Guid CartId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddShippingAddressValidator>, ICommand<Command.AddShippingAddress>
    {
        public Command.AddShippingAddress Command => new(CartId, Address);
    }

    public record AddBillingAddress(IBus Bus, Guid CartId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddBillingAddressValidator>, ICommand<Command.AddBillingAddress>
    {
        public Command.AddBillingAddress Command => new(CartId, Address);
    }

    public record AddCreditCard(IBus Bus, Guid CartId, Payloads.AddCreditCardPayload Payload, CancellationToken CancellationToken)
        : Validatable<AddCreditCardValidator>, ICommand<Command.AddCreditCard>
    {
        public Command.AddCreditCard Command => new(CartId, Payload.Amount, Payload.CreditCard);
    }

    public record AddDebitCard(IBus Bus, Guid CartId, Payloads.AddDebitCardPayload Payload, CancellationToken CancellationToken)
        : Validatable<AddDebitCardValidator>, ICommand<Command.AddDebitCard>
    {
        public Command.AddDebitCard Command => new(CartId, Payload.Amount, Payload.DebitCard);
    }

    public record AddPayPal(IBus Bus, Guid CartId, Payloads.AddPaypalPayload Payload, CancellationToken CancellationToken)
        : Validatable<AddPayPalValidator>, ICommand<Command.AddPayPal>
    {
        public Command.AddPayPal Command => new(CartId, Payload.Amount, Payload.PayPal);
    }

    public record RemovePaymentMethod(IBus Bus, Guid CartId, Guid MethodId, CancellationToken CancellationToken)
        : Validatable<RemovePaymentMethodValidator>, ICommand<Command.RemovePaymentMethod>
    {
        public Command.RemovePaymentMethod Command => new(CartId, MethodId);
    }

    public record RebuildProjection(IBus Bus, string Name, CancellationToken CancellationToken)
        : Validatable<RebuildProjectionValidator>, ICommand<Command.RebuildProjection>
    {
        public Command.RebuildProjection Command => new(Name);
    }
}