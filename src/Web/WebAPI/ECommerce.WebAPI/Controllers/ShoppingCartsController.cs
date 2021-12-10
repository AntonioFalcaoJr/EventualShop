using System;
using System.Threading;
using System.Threading.Tasks;
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
        => GetQueryResponseAsync<Queries.GetShoppingCart, Responses.CartDetails>(new(customerId), cancellationToken);

    [HttpPost("{customerId:guid}")]
    public Task<IActionResult> CreateCartAsync(Guid customerId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.CreateCart>(new(customerId), cancellationToken);

    [HttpPut("{cartId:guid}/[action]")]
    public Task<IActionResult> CheckOutAsync(Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.CheckOutCart>(new(cartId), cancellationToken);

    [HttpDelete("{cartId:guid}")]
    public Task<IActionResult> DiscardCartAsync(Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.DiscardCart>(new(cartId), cancellationToken);

    [HttpGet("{cartId:guid}/items")]
    public Task<IActionResult> GetShoppingCartItemsAsync(Guid cartId, CancellationToken cancellationToken)
        => GetQueryResponseAsync<Queries.GetShoppingCart, Responses.CartDetails>(new(cartId), cancellationToken);

    [HttpPost("{cartId:guid}/items")]
    public Task<IActionResult> AddCartItemAsync(Guid cartId, Models.Item item, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddCartItem>(new(cartId, item), cancellationToken);

    [HttpGet("{cartId:guid}/items/{productId:guid}")]
    public Task<IActionResult> GetShoppingCartItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken)
        => GetQueryResponseAsync<Queries.GetShoppingCart, Responses.CartDetails>(new(cartId), cancellationToken);

    [HttpDelete("{cartId:guid}/items/{productId:guid}")]
    public Task<IActionResult> RemoveCartItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.RemoveCartItem>(new(cartId, productId), cancellationToken);

    [HttpPut("{cartId:guid}/items/{productId:guid}/[action]")]
    public Task<IActionResult> UpdateQuantityAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.UpdateCartItemQuantity>(new(cartId, productId, quantity), cancellationToken);

    [HttpPost("{cartId:guid}/customers/[action]")]
    public Task<IActionResult> AddShippingAddressAsync(Guid cartId, Models.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddShippingAddress>(new(cartId, address), cancellationToken);

    [HttpPut("{cartId:guid}/customers/[action]")]
    public Task<IActionResult> ChangeBillingAddressAsync(Guid cartId, Models.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.ChangeBillingAddress>(new(cartId, address), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/[action]")]
    public Task<IActionResult> AddCreditCardAsync(Guid cartId, Models.CreditCard creditCard, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddCreditCard>(new(cartId, creditCard), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/[action]")]
    public Task<IActionResult> AddPayPalAsync(Guid cartId, Models.PayPal payPal, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddPayPal>(new(cartId, payPal), cancellationToken);
}