using Asp.Versioning.Builder;
using Contracts.Services.ShoppingCart;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.Validations;

namespace WebAPI.APIs.ShoppingCarts;

public static class ShoppingCartApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/shopping-carts/";

    public static IVersionedEndpointRouteBuilder MapShoppingCartApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/{customerId:guid}", (IBus bus, Guid customerId, CancellationToken cancellationToken)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetCustomerShoppingCart, Projection.ShoppingCart>(bus, new(customerId), cancellationToken));

        group.MapPost("/", ([AsParameters] Requests.CreateCart request)
            => ApplicationApi.SendCommandAsync<Command.CreateCart>(request));

        group.MapGet("/{cartId:guid}", (IBus bus, [NotEmpty] Guid cartId, CancellationToken cancellationToken)
            => ApplicationApi.GetProjectionAsync<Query.GetShoppingCart, Projection.ShoppingCart>(bus, new(cartId), cancellationToken));

        group.MapPut("/{cartId:guid}/check-out", ([AsParameters] Requests.CheckOut request)
            => ApplicationApi.SendCommandAsync<Command.CheckOutCart>(request));

        group.MapPut("/{cartId:guid}/add-shipping-address", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

        group.MapPut("/{cartId:guid}/add-billing-address", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));

        group.MapGet("/{cartId:guid}/items", (IBus bus, Guid cartId, ushort? limit, ushort? offset, CancellationToken cancellationToken)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetShoppingCartItems, Projection.ShoppingCartItem>(bus, new(cartId, limit ?? 0, offset ?? 0), cancellationToken));

        group.MapPost("/{cartId:guid}/items", ([AsParameters] Requests.AddCartItem request)
            => ApplicationApi.SendCommandAsync<Command.AddCartItem>(request));

        group.MapGet("/{cartId:guid}/items/{itemId:guid}", (IBus bus, [NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
            => ApplicationApi.GetProjectionAsync<Query.GetShoppingCartItem, Projection.ShoppingCartItem>(bus, new(cartId, itemId), cancellationToken));

        group.MapDelete("/{cartId:guid}/items/{itemId:guid}", ([AsParameters] Requests.RemoveCartItem request)
            => ApplicationApi.SendCommandAsync<Command.RemoveCartItem>(request));

        group.MapPut("/{cartId:guid}/items/{itemId:guid}/change-quantity", ([AsParameters] Requests.ChangeCartItemQuantity request)
            => ApplicationApi.SendCommandAsync<Command.ChangeCartItemQuantity>(request));

        group.MapGet("/{cartId:guid}/payment-methods", (IBus bus, [NotEmpty] Guid cartId, ushort limit, ushort offset, CancellationToken cancellationToken)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetCartPaymentMethods, Projection.PaymentMethod>(bus, new(cartId, limit, offset), cancellationToken));

        group.MapGet("/{cartId:guid}/payment-methods/{methodId:guid}", (IBus bus, [NotEmpty] Guid cartId, [NotEmpty] Guid methodId, CancellationToken cancellationToken)
            => ApplicationApi.GetProjectionAsync<Query.GetCartPaymentMethod, Projection.PaymentMethod>(bus, new(cartId, methodId), cancellationToken));

        group.MapDelete("/{cartId:guid}/payment-methods/{methodId:guid}", ([AsParameters] Requests.RemovePaymentMethod request)
            => ApplicationApi.SendCommandAsync<Command.RemovePaymentMethod>(request));

        group.MapPost("/{cartId:guid}/payment-methods/credit-card", ([AsParameters] Requests.AddCreditCard request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapPost("/{cartId:guid}/payment-methods/debit-card", ([AsParameters] Requests.AddDebitCard request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapPost("/{cartId:guid}/payment-methods/pay-pal", ([AsParameters] Requests.AddPayPal request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapPost("/admin/rebuild-projection", ([AsParameters] Requests.RebuildProjection request)
            => ApplicationApi.SendCommandAsync<Command.RebuildProjection>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapShoppingCartApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/{customerId:guid}", (IBus bus, Guid customerId, CancellationToken cancellationToken)
            => ApplicationApi.GetPagedProjectionAsync<Query.GetCustomerShoppingCart, Projection.ShoppingCart>(bus, new(customerId), cancellationToken));

        return builder;
    }
}