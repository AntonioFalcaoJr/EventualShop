using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using ECommerce.WebAPI.Abstractions;
using ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;
using ECommerce.WebAPI.ValidationAttributes;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[Route("api/v1/[controller]")]
public class ShoppingCartsController : ApplicationController
{
    private readonly IMapper _mapper;

    public ShoppingCartsController(IBus bus, IMapper mapper)
        : base(bus, mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{cartId:guid}")]
    [ProducesResponseType(typeof(Dtos.Cart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetShoppingCartAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCart, Responses.Cart, Dtos.Cart>(new(cartId), cancellationToken);

    [HttpDelete("{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DiscardCartAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.DiscardCart>(new(cartId), cancellationToken);

    [HttpGet]
    [ProducesResponseType(typeof(Dtos.Cart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetCustomerShoppingCartAsync([Required, NotEmpty] Guid customerId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetCustomerShoppingCart, Responses.Cart, Dtos.Cart>(new(customerId), cancellationToken);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateCartAsync([FromBody] Requests.CreateCart request, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.CreateCart>(new(request.CustomerId), cancellationToken);

    [HttpPut("{cartId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CheckOutAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.CheckOutCart>(new(cartId), cancellationToken);

    [HttpGet("{cartId:guid}/items")]
    [ProducesResponseType(typeof(Outputs.CartItemsPagedResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetShoppingCartItemsAsync([NotEmpty] Guid cartId, int limit, int offset, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCartItems, Responses.CartItemsPagedResult, Outputs.CartItemsPagedResult>(new(cartId, limit, offset), cancellationToken);

    [HttpPost("{cartId:guid}/items")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddCartItemAsync(Guid cartId, [FromBody] Dtos.Item item, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddCartItem>(new(cartId, _mapper.Map<Models.Item>(item)), cancellationToken);

    //
    [HttpGet("{cartId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(typeof(Dtos.Item), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetShoppingCartItemAsync(Guid cartId, Guid itemId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCartItem, Responses.CartItem, Dtos.Item>(new(cartId, itemId), cancellationToken);
    //

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