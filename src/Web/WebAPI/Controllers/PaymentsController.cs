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
        => GetProjectionAsync<Queries.GetPaymentDetails, Projections.Payment>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> RequestPayment(Commands.RequestPayment command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> CancelPayment(Commands.CancelPayment command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}