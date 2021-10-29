using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Messages.Orders;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

public class OrdersController : ApplicationController
{
    public OrdersController(IBus bus)
        : base(bus) { }

    [HttpPost]
    public Task<IActionResult> PlaceOrder(Commands.PlaceOrder command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}