using Asp.Versioning.Builder;
using Contracts.Services.ShoppingCart;
using Contracts.Services.ShoppingCart.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.ShoppingCarts;

public static class ShoppingCartApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/shopping-carts/";

    public static IVersionedEndpointRouteBuilder MapShoppingCartApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("/", ([AsParameters] Requests.CreateCart request)
            => ApplicationApi.SendCommandAsync<Command.CreateCart>(request));

        group.MapPut("/{cartId:guid}/check-out", ([AsParameters] Requests.CheckOut request)
            => ApplicationApi.SendCommandAsync<Command.CheckOutCart>(request));

        group.MapPut("/{cartId:guid}/add-shipping-address", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

        group.MapPut("/{cartId:guid}/add-billing-address", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));
        
        group.MapGet("/{cartId:guid}/details", ([AsParameters] Requests.GetShoppingCartDetails request)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (request, (client, ct) => client.GetShoppingCartDetailsAsync(request, cancellationToken: ct)));

        group.MapPost("/{cartId:guid}/items", ([AsParameters] Requests.AddCartItem request)
            => ApplicationApi.SendCommandAsync<Command.AddCartItem>(request));
        
        group.MapGet("/{cartId:guid}/items/list-item", ([AsParameters] Requests.ListShoppingCartItemsListItems request)
            => ApplicationApi.ListAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartItemListItem>
                (request, (client, ct) => client.ListShoppingCartItemsListItemsAsync(request, cancellationToken: ct)));

        group.MapDelete("/{cartId:guid}/items/{itemId:guid}", ([AsParameters] Requests.RemoveCartItem request)
            => ApplicationApi.SendCommandAsync<Command.RemoveCartItem>(request));
        
        group.MapGet("/{cartId:guid}/items/{itemId:guid}/details", ([AsParameters] Requests.GetShoppingCartItemDetails request)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (request, (client, ct) => client.GetShoppingCartItemDetailsAsync(request, cancellationToken: ct)));

        group.MapPut("/{cartId:guid}/items/{itemId:guid}/change-quantity", ([AsParameters] Requests.ChangeCartItemQuantity request)
            => ApplicationApi.SendCommandAsync<Command.ChangeCartItemQuantity>(request));

        group.MapGet("/{cartId:guid}/payment-methods/list-items", ([AsParameters] Requests.ListPaymentMethodsListItems request)
            => ApplicationApi.ListAsync<ShoppingCartService.ShoppingCartServiceClient, PaymentMethodListItem>
                (request, (client, ct) => client.ListPaymentMethodsListItemsAsync(request, cancellationToken: ct)));

        group.MapPost("/{cartId:guid}/payment-methods/credit-card", ([AsParameters] Requests.AddCreditCard request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapPost("/{cartId:guid}/payment-methods/debit-card", ([AsParameters] Requests.AddDebitCard request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapPost("/{cartId:guid}/payment-methods/pay-pal", ([AsParameters] Requests.AddPayPal request)
            => ApplicationApi.SendCommandAsync<Command.AddPaymentMethod>(request));

        group.MapDelete("/{cartId:guid}/payment-methods/{methodId:guid}", ([AsParameters] Requests.RemovePaymentMethod request)
            => ApplicationApi.SendCommandAsync<Command.RemovePaymentMethod>(request));

        group.MapGet("/{cartId:guid}/payment-methods/{methodId:guid}/details", ([AsParameters] Requests.GetPaymentMethodDetails request)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, PaymentMethodDetails>
                (request, (client, ct) => client.GetPaymentMethodDetailsAsync(request, cancellationToken: ct)));

        group.MapGet("/{cartId:guid}/customers/{customerId:guid}/details", ([AsParameters] Requests.GetCustomerShoppingCartDetails request)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (request, (client, ct) => client.GetCustomerShoppingCartDetailsAsync(request, cancellationToken: ct)));

        group.MapPost("/admin/rebuild-projection", ([AsParameters] Requests.RebuildProjection request)
            => ApplicationApi.SendCommandAsync<Command.RebuildProjection>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapShoppingCartApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/{cartId:guid}/details", ([AsParameters] Requests.GetShoppingCartDetails request)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (request, (client, ct) => client.GetShoppingCartDetailsAsync(request, cancellationToken: ct)));

        return builder;
    }
}