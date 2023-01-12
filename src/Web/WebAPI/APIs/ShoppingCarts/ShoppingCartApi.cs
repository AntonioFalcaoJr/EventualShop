using Asp.Versioning.Builder;
using Contracts.Services.ShoppingCart.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.ShoppingCarts;

public static class ShoppingCartApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/shopping-carts/";

    public static IVersionedEndpointRouteBuilder MapShoppingCartApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("/", ([AsParameters] Commands.CreateCart createCart)
            => ApplicationApi.SendCommandAsync(createCart));

        group.MapPut("/{cartId:guid}/check-out", ([AsParameters] Commands.CheckOut checkOut)
            => ApplicationApi.SendCommandAsync(checkOut));

        group.MapPut("/{cartId:guid}/add-shipping-address", ([AsParameters] Commands.AddShippingAddress addShippingAddress)
            => ApplicationApi.SendCommandAsync(addShippingAddress));

        group.MapPut("/{cartId:guid}/add-billing-address", ([AsParameters] Commands.AddBillingAddress addBillingAddress)
            => ApplicationApi.SendCommandAsync(addBillingAddress));

        group.MapGet("/{cartId:guid}/details", ([AsParameters] Queries.GetShoppingCartDetails query)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (query, (client, ct) => client.GetShoppingCartDetailsAsync(query, cancellationToken: ct)));

        group.MapPost("/{cartId:guid}/items", ([AsParameters] Commands.AddCartItem request)
            => ApplicationApi.SendCommandAsync(request));

        group.MapGet("/{cartId:guid}/items/list-item", ([AsParameters] Queries.ListShoppingCartItemsListItems query)
            => ApplicationApi.ListAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartItemListItem>
                (query, (client, ct) => client.ListShoppingCartItemsListItemsAsync(query, cancellationToken: ct)));

        group.MapDelete("/{cartId:guid}/items/{itemId:guid}", ([AsParameters] Commands.RemoveCartItem request)
            => ApplicationApi.SendCommandAsync(request));

        group.MapGet("/{cartId:guid}/items/{itemId:guid}/details", ([AsParameters] Queries.GetShoppingCartItemDetails query)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (query, (client, ct) => client.GetShoppingCartItemDetailsAsync(query, cancellationToken: ct)));

        group.MapPut("/{cartId:guid}/items/{itemId:guid}/change-quantity", ([AsParameters] Commands.ChangeCartItemQuantity changeCartItemQuantity)
            => ApplicationApi.SendCommandAsync(changeCartItemQuantity));

        group.MapPost("/{cartId:guid}/payment-methods/credit-card", ([AsParameters] Commands.AddCreditCard addCreditCard)
            => ApplicationApi.SendCommandAsync(addCreditCard));

        group.MapPost("/{cartId:guid}/payment-methods/debit-card", ([AsParameters] Commands.AddDebitCard addDebitCard)
            => ApplicationApi.SendCommandAsync(addDebitCard));

        group.MapPost("/{cartId:guid}/payment-methods/pay-pal", ([AsParameters] Commands.AddPayPal addPayPal)
            => ApplicationApi.SendCommandAsync(addPayPal));
        
        group.MapGet("/{cartId:guid}/payment-methods/list-items", ([AsParameters] Queries.ListPaymentMethodsListItems query)
            => ApplicationApi.ListAsync<ShoppingCartService.ShoppingCartServiceClient, PaymentMethodListItem>
                (query, (client, ct) => client.ListPaymentMethodsListItemsAsync(query, cancellationToken: ct)));

        group.MapDelete("/{cartId:guid}/payment-methods/{methodId:guid}", ([AsParameters] Commands.RemovePaymentMethod removePaymentMethod)
            => ApplicationApi.SendCommandAsync(removePaymentMethod));

        group.MapGet("/{cartId:guid}/payment-methods/{methodId:guid}/details", ([AsParameters] Queries.GetPaymentMethodDetails query)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, PaymentMethodDetails>
                (query, (client, ct) => client.GetPaymentMethodDetailsAsync(query, cancellationToken: ct)));

        group.MapGet("/customers/{customerId:guid}/cart-details", ([AsParameters] Queries.GetCustomerShoppingCartDetails query)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (query, (client, ct) => client.GetCustomerShoppingCartDetailsAsync(query, cancellationToken: ct)));

        group.MapPost("/admin/rebuild-projection", ([AsParameters] Commands.RebuildProjection rebuildProjection)
            => ApplicationApi.SendCommandAsync(rebuildProjection));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapShoppingCartApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/{cartId:guid}/details", ([AsParameters] Queries.GetShoppingCartDetails query)
            => ApplicationApi.GetAsync<ShoppingCartService.ShoppingCartServiceClient, ShoppingCartDetails>
                (query, (client, ct) => client.GetShoppingCartDetailsAsync(query, cancellationToken: ct)));

        return builder;
    }
}