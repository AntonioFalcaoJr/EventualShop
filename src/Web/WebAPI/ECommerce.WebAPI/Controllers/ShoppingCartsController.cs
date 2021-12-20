using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[Route("api/v1/[controller]")]
public class ShoppingCartsController : ApplicationController
{
    public ShoppingCartsController(IBus bus)
        : base(bus) { }

    [HttpGet("{customerId:guid}")]
    public Task<IActionResult> GetShoppingCartAsync(Guid customerId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCart, Responses.CartDetails>(new(customerId), cancellationToken);

    [HttpPost]
    public Task<IActionResult> CreateCartAsync(Commands.CreateCart command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut("{cartId:guid}/[action]")]
    public Task<IActionResult> CheckOutAsync(Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.CheckOutCart>(new(cartId), cancellationToken);

    [HttpDelete("{cartId:guid}")]
    public Task<IActionResult> DiscardCartAsync(Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.DiscardCart>(new(cartId), cancellationToken);

    [HttpGet("{cartId:guid}/items")]
    public Task<IActionResult> GetShoppingCartItemsAsync(Guid cartId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCart, Responses.CartDetails>(new(cartId), cancellationToken);

    [HttpPost("{cartId:guid}/items")]
    public Task<IActionResult> AddCartItemAsync(Guid cartId, [FromBody] Models.Item item, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddCartItem>(new(cartId, item), cancellationToken);

    [HttpGet("{cartId:guid}/items/{itemId:guid}")]
    public Task<IActionResult> GetShoppingCartItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCart, Responses.CartDetails>(new(cartId), cancellationToken);

    [HttpDelete("{cartId:guid}/items/{itemId:guid}")]
    public Task<IActionResult> RemoveCartItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.RemoveCartItem>(new(cartId, productId), cancellationToken);

    [HttpPut("{cartId:guid}/items/{itemId:guid}/[action]")]
    public Task<IActionResult> IncreaseQuantityAsync(Guid cartId, Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.IncreaseCartItemQuantity>(new(cartId, itemId), cancellationToken);

    [HttpPut("{cartId:guid}/items/{itemId:guid}/[action]")]
    public Task<IActionResult> DecreaseQuantityAsync(Guid cartId, Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.DecreaseCartItemQuantity>(new(cartId, itemId), cancellationToken);

    [HttpPost("{cartId:guid}/customers/shipping-address")]
    public Task<IActionResult> AddShippingAddressAsync(Guid cartId, Models.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddShippingAddress>(new(cartId, address), cancellationToken);

    [HttpPut("{cartId:guid}/customers/billing-address")]
    public Task<IActionResult> ChangeBillingAddressAsync(Guid cartId, Models.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.ChangeBillingAddress>(new(cartId, address), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/credit-card")]
    public Task<IActionResult> AddCreditCardAsync(Guid cartId, Models.CreditCard creditCard, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddCreditCard>(new(cartId, creditCard), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/pay-pal")]
    public Task<IActionResult> AddPayPalAsync(Guid cartId, Models.PayPal payPal, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddPayPal>(new(cartId, payPal), cancellationToken);
}