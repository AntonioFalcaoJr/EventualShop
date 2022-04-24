using ECommerce.Contracts.Payments;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class PaymentsController : ApplicationController
{
    public PaymentsController(IBus bus)
        : base(bus) { }
 
    [HttpGet]
    public Task<IActionResult> GetPaymentDetails([FromQuery] Queries.GetPaymentDetails query, CancellationToken cancellationToken)
        => GetProjectionAsync<Queries.GetPaymentDetails, Projection.Payment>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> RequestPayment(Command.RequestPayment command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> CancelPayment(Command.CancelPayment command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}