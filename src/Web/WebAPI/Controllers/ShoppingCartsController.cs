using System.ComponentModel.DataAnnotations;
using Contracts.Abstractions.Paging;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.ValidationAttributes;

namespace WebAPI.Controllers;

public class ShoppingCartsController : ApplicationController
{
    public ShoppingCartsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    [ProducesResponseType(typeof(Projection.ShoppingCart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetByCustomerAsync([Required, NotEmpty] Guid customerId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetCustomerShoppingCart, Projection.ShoppingCart>(new(customerId), cancellationToken);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(Request.CreateCart request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.CreateCart>(new(request.CustomerId), cancellationToken);

    [HttpGet("{cartId:guid}")]
    [ProducesResponseType(typeof(Projection.ShoppingCart), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetShoppingCart, Projection.ShoppingCart>(new(cartId), cancellationToken);

    [HttpDelete("{cartId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DiscardAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DiscardCart>(new(cartId), cancellationToken);

    [HttpPut("{cartId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CheckOutAsync([NotEmpty] Guid cartId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.CheckOutCart>(new(cartId), cancellationToken);

    [HttpGet("{cartId:guid}/items")]
    [ProducesResponseType(typeof(IPagedResult<Projection.ShoppingCartItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync([NotEmpty] Guid cartId, int limit, int offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetShoppingCartItems, IPagedResult<Projection.ShoppingCartItem>>(new(cartId, limit, offset), cancellationToken);

    [HttpPost("{cartId:guid}/items")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddAsync([NotEmpty] Guid cartId, Request.AddShoppingCartItem request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.AddCartItem>(new(cartId, request.Product, request.Quantity), cancellationToken);

    [HttpGet("{cartId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(typeof(Projection.ShoppingCartItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetShoppingCartItem, Projection.ShoppingCartItem>(new(cartId, itemId), cancellationToken);

    [HttpDelete("{cartId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> RemoveAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.RemoveCartItem>(new(cartId, itemId), cancellationToken);

    [HttpPut("{cartId:guid}/items/{itemId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> IncreaseAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.IncreaseCartItem>(new(cartId, itemId), cancellationToken);

    [HttpPut("{cartId:guid}/items/{itemId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DecreaseAsync([NotEmpty] Guid cartId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DecreaseCartItem>(new(cartId, itemId), cancellationToken);

    [HttpPost("{cartId:guid}/customers/shipping-address")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddAsync([NotEmpty] Guid cartId, Dto.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Command.AddShippingAddress>(new(cartId, address), cancellationToken);

    [HttpPut("{cartId:guid}/customers/billing-address")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ChangeAsync([NotEmpty] Guid cartId, Dto.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Command.ChangeBillingAddress>(new(cartId, address), cancellationToken);

    [HttpGet("{cartId:guid}/payment-methods")]
    [ProducesResponseType(typeof(IPagedResult<Projection.PaymentMethod>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetPaymentMethodsAsync([NotEmpty] Guid cartId, int limit, int offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetShoppingCartPaymentMethods, IPagedResult<Projection.PaymentMethod>>(new(cartId, limit, offset), cancellationToken);

    [HttpGet("{cartId:guid}/payment-methods/{paymentMethodId:guid}")]
    [ProducesResponseType(typeof(Projection.PaymentMethod), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetPaymentMethodAsync([NotEmpty] Guid cartId, [NotEmpty] Guid paymentMethodId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetShoppingCartPaymentMethod, Projection.PaymentMethod>(new(cartId, paymentMethodId), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/credit-card")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddAsync([NotEmpty] Guid cartId, decimal amount, Dto.CreditCard creditCard, CancellationToken cancellationToken)
        => SendCommandAsync<Command.AddCreditCard>(new(cartId, amount, creditCard), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/debit-card")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddAsync([NotEmpty] Guid cartId, decimal amount, Dto.DebitCard debitCard, CancellationToken cancellationToken)
        => SendCommandAsync<Command.AddDebitCard>(new(cartId, amount, debitCard), cancellationToken);

    [HttpPost("{cartId:guid}/payment-methods/pay-pal")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddAsync([NotEmpty] Guid cartId, decimal amount, Dto.PayPal payPal, CancellationToken cancellationToken)
        => SendCommandAsync<Command.AddPayPal>(new(cartId, amount, payPal), cancellationToken);
}