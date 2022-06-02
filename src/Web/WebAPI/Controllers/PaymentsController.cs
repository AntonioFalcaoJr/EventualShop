using Contracts.Services.Payment;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

public class PaymentsController : ApplicationController
{
    public PaymentsController(IBus bus)
        : base(bus) { }

    [HttpGet("{paymentId:guid}")]
    [ProducesResponseType(typeof(Projection.Payment), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetPaymentDetails(Guid paymentId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetPayment, Projection.Payment>(new(paymentId), cancellationToken);
}