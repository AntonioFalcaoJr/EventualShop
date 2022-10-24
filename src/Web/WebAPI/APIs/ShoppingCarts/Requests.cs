using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.ShoppingCarts.Validators;

namespace WebAPI.APIs.ShoppingCarts;

public static class Requests
{
    public record CreateCart(IBus Bus, Guid CustomerId, CancellationToken CancellationToken)
        : Validatable<CreateCartValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.CreateCart(Guid.NewGuid(), CustomerId);
    }

    public record AddCartItem(IBus Bus, Guid CartId, Payloads.AddCartItemPayload Payload, CancellationToken CancellationToken)
        : Validatable<AddCartItemValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddCartItem(CartId, Guid.NewGuid(), Payload.CatalogId, Payload.InventoryId, Payload.Product, Payload.Quantity, Payload.UnitPrice);
    }

    public record CheckOut(IBus Bus, Guid CartId, CancellationToken CancellationToken)
        : Validatable<CheckOutValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.CheckOutCart(CartId);
    }

    public record ChangeCartItemQuantity(IBus Bus, Guid CartId, Guid ItemId, ushort Quantity, CancellationToken CancellationToken)
        : Validatable<ChangeCartItemQuantityValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ChangeCartItemQuantity(CartId, ItemId, Quantity);
    }

    public record RemoveCartItem(IBus Bus, Guid CartId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<RemoveCartItemValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.RemoveCartItem(CartId, ItemId);
    }

    public record AddShippingAddress(IBus Bus, Guid CartId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddShippingAddressValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddShippingAddress(CartId, Address);
    }

    public record AddBillingAddress(IBus Bus, Guid CartId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddBillingAddressValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddBillingAddress(CartId, Address);
    }

    public record AddCreditCard(IBus Bus, Guid CartId, decimal Amount, Dto.CreditCard CreditCard, CancellationToken CancellationToken)
        : Validatable<AddCreditCardValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddPaymentMethod(CartId, Amount, CreditCard);
    }

    public record AddDebitCard(IBus Bus, Guid CartId, decimal Amount, Dto.DebitCard DebitCard, CancellationToken CancellationToken)
        : Validatable<AddDebitCardValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddPaymentMethod(CartId, Amount, DebitCard);
    }

    public record AddPayPal(IBus Bus, Guid CartId, decimal Amount, Dto.PayPal PayPal, CancellationToken CancellationToken)
        : Validatable<AddPayPalValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddPaymentMethod(CartId, Amount, PayPal);
    }

    public record RemovePaymentMethod(IBus Bus, Guid CartId, Guid MethodId, CancellationToken CancellationToken)
        : Validatable<RemovePaymentMethodValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.RemovePaymentMethod(CartId, MethodId);
    }
    
    public record RebuildProjection(IBus Bus, string Name, CancellationToken CancellationToken)
        : Validatable<RebuildProjectionValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.RebuildProjection(Name);
    }
}