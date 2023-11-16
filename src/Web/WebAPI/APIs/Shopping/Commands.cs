using Contracts.Boundaries.Shopping.ShoppingCart;
using Contracts.Shopping.Commands;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Shopping.Validators;
using static Contracts.Shopping.Commands.ShoppingCommandService;

namespace WebAPI.APIs.Shopping;

public static class Commands
{
    // public record StartShopping(IBus Bus, string CustomerId, CancellationToken CancellationToken)
    //     : TheCommand<StartShoppingCommand, CreateCartValidator>(default, Bus, CancellationToken)
    // {
    //     public static implicit operator Command.StartShopping(StartShopping request)
    //         => new(request.Payload.CustomerId, request.Payload.Currency);
    // }

    // public record AddCartItem(IBus Bus, string CartId, Payloads.AddCartItemPayload Payload, CancellationToken CancellationToken)
    //     : Validatable<AddCartItemValidator>, ICommand<Command.AddCartItem>
    // {
    //     public Command.AddCartItem Command
    //         => new(CartId, Payload.CatalogId.ToString(), Payload.InventoryId.ToString(), Payload.Product, Payload.Quantity, Payload.UnitPrice);
    // }

    public record StartShopping(ShoppingCommandServiceClient Client, string CustomerId, CancellationToken CancellationToken)
        : Validatable<StartShoppingValidator>, INewCommand<ShoppingCommandServiceClient>
    {
        public static implicit operator StartShoppingCommand(StartShopping request)
            => new() { CustomerId = request.CustomerId };
    }
    
    public record StartShopping2(ShoppingCommandServiceClient Client, string CustomerId, CancellationToken CancellationToken)
        : IVeryNewCommand<ShoppingCommandServiceClient, StartShoppingValidator>
    {
        public static implicit operator StartShoppingCommand(StartShopping2 request)
            => new() { CustomerId = request.CustomerId };
    }

    public record CheckOut(IBus Bus, string CartId, CancellationToken CancellationToken)
        : Validatable<CheckOutValidator>, ICommand<Command.CheckOutCart>
    {
        public Command.CheckOutCart Command => new(CartId);
    }

    public record ChangeCartItemQuantity(IBus Bus, string CartId, string ItemId, ushort Quantity, CancellationToken CancellationToken)
        : Validatable<ChangeCartItemQuantityValidator>, ICommand<Command.ChangeCartItemQuantity>
    {
        public Command.ChangeCartItemQuantity Command => new(CartId, ItemId, Quantity);
    }

    public record RemoveCartItem(IBus Bus, string CartId, string ItemId, CancellationToken CancellationToken)
        : Validatable<RemoveCartItemValidator>, ICommand<Command.RemoveCartItem>
    {
        public Command.RemoveCartItem Command => new(CartId, ItemId);
    }

    // public record AddShippingAddress(IBus Bus, string CartId, Dto.Address Address, CancellationToken CancellationToken)
    //     : Validatable<AddShippingAddressValidator>, ICommand<Contracts.Boundaries.Shopping.Checkout.Command.AddShippingAddress>
    // {
    //     public Contracts.Boundaries.Shopping.Checkout.Command.AddShippingAddress Command => new(CartId, Address);
    // }
    //
    // public record AddBillingAddress(IBus Bus, string CartId, Dto.Address Address, CancellationToken CancellationToken)
    //     : Validatable<AddBillingAddressValidator>, ICommand<Contracts.Boundaries.Shopping.Checkout.Command.AddBillingAddress>
    // {
    //     public Contracts.Boundaries.Shopping.Checkout.Command.AddBillingAddress Command => new(CartId, Address);
    // }

    // public record AddCreditCard(IBus Bus, Guid CartId, Payloads.AddCreditCardPayload Payload, CancellationToken CancellationToken)
    //     : Validatable<AddCreditCardValidator>, ICommand<Contracts.Services.Checkout.Commands.Command.AddCreditCard>
    // {
    //     public Contracts.Services.Checkout.Commands.Command.AddCreditCard Command => new(CartId, Payload.Amount, Payload.CreditCard);
    // }

    // public record AddDebitCard(IBus Bus, string CartId, Payloads.AddDebitCardPayload Payload, CancellationToken CancellationToken)
    //     : Validatable<AddDebitCardValidator>, ICommand<Contracts.Boundaries.Shopping.Checkout.Command.AddDebitCard>
    // {
    //     public Contracts.Boundaries.Shopping.Checkout.Command.AddDebitCard Command => new(CartId, Payload.Amount, Payload.DebitCard);
    // }

    // public record RemovePaymentMethod(IBus Bus, string CartId, string MethodId, CancellationToken CancellationToken)
    //     : Validatable<RemovePaymentMethodValidator>, ICommand<Command.RemovePaymentMethod>
    // {
    //     public Command.RemovePaymentMethod Command => new(CartId, MethodId);
    // }

    public record RebuildProjection(IBus Bus, string Name, CancellationToken CancellationToken)
        : Validatable<RebuildProjectionValidator>, ICommand<Command.RebuildCartProjection>
    {
        public Command.RebuildCartProjection Command => new(Name);
    }
}