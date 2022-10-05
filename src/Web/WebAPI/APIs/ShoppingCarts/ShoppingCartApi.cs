using Contracts.Services.ShoppingCart;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.Validations;

namespace WebAPI.APIs.ShoppingCarts;

public static class ShoppingCartApi
{
    public static RouteGroupBuilder MapShoppingCartApi(this RouteGroupBuilder group)
    {
        group.MapGet("/{customerId:guid}", (IBus bus, Guid customerId, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetCustomerShoppingCart, Projection.ShoppingCart>(bus, new(customerId), ct));

        group.MapPost("/", ([AsParameters] Requests.CreateCart request)
            => ApplicationApi.SendCommandAsync<Command.CreateCart>(request));

        group.MapGet("/{cartId:guid}", (IBus bus, [NotEmpty] Guid cartId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetShoppingCart, Projection.ShoppingCart>(bus, new(cartId), ct));

        group.MapPut("/{cartId:guid}/check-out", ([AsParameters] Requests.CheckOut request)
            => ApplicationApi.SendCommandAsync<Command.CheckOutCart>(request));

        group.MapGet("/{cartId:guid}/items", (IBus bus, Guid cartId, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetShoppingCartItems, Projection.ShoppingCartItem>(bus, new(cartId, limit ?? 0, offset ?? 0), ct));

        group.MapPost("/{cartId:guid}/items", ([AsParameters] Requests.AddCartItem request)
            => ApplicationApi.SendCommandAsync<Command.AddCartItem>(request));

        group.MapGet("/{cartId:guid}/items/{itemId:guid}", (IBus bus, [NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetShoppingCartItem, Projection.ShoppingCartItem>(bus, new(cartId, itemId), ct));

        group.MapDelete("/{cartId:guid}/items/{itemId:guid}", ([AsParameters] Requests.RemoveCartItem request)
            => ApplicationApi.SendCommandAsync<Command.RemoveCartItem>(request));

        group.MapPut("/{cartId:guid}/items/{itemId:guid}/change-quantity", ([AsParameters] Requests.ChangeCartItemQuantity request)
            => ApplicationApi.SendCommandAsync<Command.ChangeCartItemQuantity>(request));

        group.MapPut("/{cartId:guid}/customers/shipping-address", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

        group.MapPut("/{cartId:guid}/customers/billing-address", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));

        group.MapGet("/{cartId:guid}/payment-methods", (IBus bus, [NotEmpty] Guid cartId, ushort limit, ushort offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetCartPaymentMethods, Projection.PaymentMethod>(bus, new(cartId, limit, offset), ct));

        group.MapGet("/{cartId:guid}/payment-methods/{methodId:guid}", (IBus bus, [NotEmpty] Guid cartId, [NotEmpty] Guid methodId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetCartPaymentMethod, Projection.PaymentMethod>(bus, new(cartId, methodId), ct));

        group.MapPut("/{cartId:guid}/payment-methods/credit-card", ([AsParameters] Requests.AddCreditCard request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapPut("/{cartId:guid}/payment-methods/debit-card", ([AsParameters] Requests.AddDebitCard request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapPut("/{cartId:guid}/payment-methods/pay-pal", ([AsParameters] Requests.AddPayPal request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        return group.WithMetadata(new TagsAttribute("ShoppingCarts"));
    }
}