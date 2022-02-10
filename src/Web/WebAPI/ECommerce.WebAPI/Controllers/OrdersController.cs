using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Contracts.Order;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class OrdersController : ApplicationController
{
    public OrdersController(IBus bus, IMapper mapper)
        : base(bus, mapper) { }

    [HttpPost]
    public Task<IActionResult> PlaceOrder(Commands.PlaceOrder command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}