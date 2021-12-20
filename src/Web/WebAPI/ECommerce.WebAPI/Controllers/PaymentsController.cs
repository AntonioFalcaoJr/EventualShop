using ECommerce.Contracts.Payment;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
public class PaymentsController : ApplicationController
{
    public PaymentsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public Task<IActionResult> GetPaymentDetails([FromQuery] Queries.GetPaymentDetails query, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetPaymentDetails, Responses.PaymentDetails>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> RequestPayment(Commands.RequestPayment command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> CancelPayment(Commands.CancelPayment command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}