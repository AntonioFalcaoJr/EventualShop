using ECommerce.Contracts.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class OrdersController : ApplicationController
{
    public OrdersController(IBus bus)
        : base(bus) { }

    [HttpPost]
    public Task<IActionResult> PlaceOrder(Command.PlaceOrder command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}