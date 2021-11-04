using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Messages.Services.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

public class ShoppingCartsController : ApplicationController
{
    public ShoppingCartsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public Task<IActionResult> GetCart([FromQuery] Queries.GetShoppingCart query, CancellationToken cancellationToken)
        => GetQueryResponseAsync<Queries.GetShoppingCart, Responses.CartDetails>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> CreateCart(Commands.CreateCart command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> AddCartItem(Commands.AddCartItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> AddCreditCard(Commands.AddCreditCard command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> AddShippingAddress(Commands.AddShippingAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> ChangeBillingAddress(Commands.ChangeBillingAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> RemoveCartItem(Commands.RemoveCartItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
        
    [HttpPut]
    public Task<IActionResult> CheckOutCart(Commands.CheckOutCart command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}