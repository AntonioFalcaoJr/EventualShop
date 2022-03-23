using AutoMapper;
using ECommerce.Contracts.Order;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class OrdersController : ApplicationController
{
    public OrdersController(IBus bus, IMapper mapper)
        : base(bus, mapper) { }

    [HttpPost]
    public Task<IActionResult> PlaceOrder(Commands.PlaceOrder command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}