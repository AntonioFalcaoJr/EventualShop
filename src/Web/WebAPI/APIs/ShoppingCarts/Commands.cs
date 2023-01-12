using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using Contracts.Services.ShoppingCart.Protobuf;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.ShoppingCarts.Validators;

namespace WebAPI.APIs.ShoppingCarts;

public static class Commands
{
    public record CreateCart(IBus Bus, Guid CustomerId, CancellationToken CancellationToken)
        : Validatable<CreateCartValidator>, ICommand<Command.CreateCart>
    {
        public Command.CreateCart Command
            => new(Guid.NewGuid(), CustomerId);
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
        public Command.CheckOutCart Command
            => new(CartId);
    }

    public record ChangeCartItemQuantity(IBus Bus, Guid CartId, Guid ItemId, ushort Quantity, CancellationToken CancellationToken)
        : Validatable<ChangeCartItemQuantityValidator>, ICommand<Command.ChangeCartItemQuantity>
    {
        public Command.ChangeCartItemQuantity Command
            => new(CartId, ItemId, Quantity);
    }

    public record RemoveCartItem(IBus Bus, Guid CartId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<RemoveCartItemValidator>, ICommand<Command.RemoveCartItem>
    {
        public Command.RemoveCartItem Command
            => new(CartId, ItemId);
    }

    public record AddShippingAddress(IBus Bus, Guid CartId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddShippingAddressValidator>, ICommand<Command.AddShippingAddress>
    {
        public Command.AddShippingAddress Command
            => new(CartId, Address);
    }

    public record AddBillingAddress(IBus Bus, Guid CartId, Dto.Address Address, CancellationToken CancellationToken)
        : Validatable<AddBillingAddressValidator>, ICommand<Command.AddBillingAddress>
    {
        public Command.AddBillingAddress Command
            => new(CartId, Address);
    }

    public record AddCreditCard(IBus Bus, Guid CartId, decimal Amount, Dto.CreditCard CreditCard, CancellationToken CancellationToken)
        : Validatable<AddCreditCardValidator>, ICommand<Command.AddCreditCard>
    {
        public Command.AddCreditCard Command
            => new(CartId, Amount, CreditCard);
    }

    public record AddDebitCard(IBus Bus, Guid CartId, decimal Amount, Dto.DebitCard DebitCard, CancellationToken CancellationToken)
        : Validatable<AddDebitCardValidator>, ICommand<Command.AddDebitCard>
    {
        public Command.AddDebitCard Command
            => new(CartId, Amount, DebitCard);
    }

    public record AddPayPal(IBus Bus, Guid CartId, decimal Amount, Dto.PayPal PayPal, CancellationToken CancellationToken)
        : Validatable<AddPayPalValidator>, ICommand<Command.AddPayPal>
    {
        public Command.AddPayPal Command
            => new(CartId, Amount, PayPal);
    }

    public record RemovePaymentMethod(IBus Bus, Guid CartId, Guid MethodId, CancellationToken CancellationToken)
        : Validatable<RemovePaymentMethodValidator>, ICommand<Command.RemovePaymentMethod>
    {
        public Command.RemovePaymentMethod Command
            => new(CartId, MethodId);
    }

    public record RebuildProjection(IBus Bus, string Name, CancellationToken CancellationToken)
        : Validatable<RebuildProjectionValidator>, ICommand<Command.RebuildProjection>
    {
        public Command.RebuildProjection Command
            => new(Name);
    }


    public record GetShoppingCartDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, CancellationToken CancellationToken)
        : Validatable<GetShoppingCartDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetShoppingCartDetailsRequest(GetShoppingCartDetails request)
            => new() { CartId = request.CartId.ToString() };
    }

    public record GetCustomerShoppingCartDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CustomerId, CancellationToken CancellationToken)
        : Validatable<GetCustomerShoppingCartDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetCustomerShoppingCartDetailsRequest(GetCustomerShoppingCartDetails request)
            => new() { CustomerId = request.CustomerId.ToString() };
    }

    public record GetShoppingCartItemDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<GetShoppingCartItemDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetShoppingCartItemDetailsRequest(GetShoppingCartItemDetails request)
            => new() { CartId = request.CartId.ToString(), ItemId = request.ItemId.ToString() };
    }

    public record GetPaymentMethodDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, Guid MethodId, CancellationToken CancellationToken)
        : Validatable<GetPaymentMethodDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetPaymentMethodDetailsRequest(GetPaymentMethodDetails request)
            => new() { CartId = request.CartId.ToString(), MethodId = request.MethodId.ToString() };
    }

    public record ListPaymentMethodsListItems(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListPaymentMethodsListItemsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator ListPaymentMethodsListItemsRequest(ListPaymentMethodsListItems request)
            => new() { CartId = request.CartId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListShoppingCartItemsListItems(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListShoppingCartItemsListItemsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator ListShoppingCartItemsListItemsRequest(ListShoppingCartItemsListItems request)
            => new() { CartId = request.CartId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}