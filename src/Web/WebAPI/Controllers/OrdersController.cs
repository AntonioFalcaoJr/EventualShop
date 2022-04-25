using ECommerce.Contracts.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.ValidationAttributes;

namespace WebAPI.Controllers;

public class OrdersController : ApplicationController
{
    public OrdersController(IBus bus)
        : base(bus) { }

    [HttpPut("{orderId:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CancelAsync([NotEmpty] Guid orderId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.CancelOrder>(new(orderId), cancellationToken);
}