using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.DataTransferObjects.ShoppingCarts;
using WebAPI.ValidationAttributes;

namespace WebAPI.Controllers;

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
    [ProducesResponseType(typeof(Outputs.ShoppingCart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetShoppingCartAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCart, Responses.ShoppingCart, Outputs.ShoppingCart>(new(cartId), cancellationToken);

    [HttpDelete("{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DiscardCartAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.DiscardCart>(new(cartId), cancellationToken);

    [HttpGet]
    [ProducesResponseType(typeof(Outputs.ShoppingCart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetCustomerShoppingCartAsync([Required, NotEmpty] Guid customerId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetCustomerShoppingCart, Responses.ShoppingCart, Outputs.ShoppingCart>(new(customerId), cancellationToken);

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
    [ProducesResponseType(typeof(Outputs.ShoppingCartItems), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetShoppingCartItemsAsync([NotEmpty] Guid cartId, int limit, int offset, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCartItems, Responses.ShoppingCartItems, Outputs.ShoppingCartItems>(new(cartId, limit, offset), cancellationToken);

    [HttpPost("{cartId:guid}/items")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddCartItemAsync([NotEmpty] Guid cartId, [FromBody] Requests.AddCartItem request, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddCartItem>(new(cartId, _mapper.Map<Models.ShoppingCartItem>(request)), cancellationToken);

    [HttpGet("{cartId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(typeof(Outputs.ShoppingCartItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetShoppingCartItemAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetShoppingCartItem, Responses.ShoppingCartItem, Outputs.ShoppingCartItem>(new(cartId, itemId), cancellationToken);

    [HttpDelete("{cartId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> RemoveCartItemAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.RemoveCartItem>(new(cartId, itemId), cancellationToken);

    [HttpPut("{cartId:guid}/items/{itemId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> IncreaseAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.IncreaseShoppingCartItem>(new(cartId, itemId), cancellationToken);

    [HttpPut("{cartId:guid}/items/{itemId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DecreaseAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.DecreaseShoppingCartItem>(new(cartId, itemId), cancellationToken);

    [HttpPost("{cartId:guid}/customers/shipping-address")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddShippingAddressAsync([NotEmpty] Guid cartId, Requests.AddAddress address, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddShippingAddress>(new(cartId, _mapper.Map<Models.Address>(address)), cancellationToken);

    [HttpPut("{cartId:guid}/customers/billing-address")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ChangeBillingAddressAsync([NotEmpty] Guid cartId, Requests.AddAddress address, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.ChangeBillingAddress>(new(cartId, _mapper.Map<Models.Address>(address)), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/credit-card")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddCreditCardAsync([NotEmpty] Guid cartId, Requests.PaymentWithCreditCard creditCard, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddCreditCard>(new(cartId, _mapper.Map<Models.CreditCard>(creditCard)), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/pay-pal")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddPayPalAsync([NotEmpty] Guid cartId, Requests.PaymentWithPayPal payPal, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.AddPayPal>(new(cartId, _mapper.Map<Models.PayPal>(payPal)), cancellationToken);
}