using Contracts.Services.Payments;
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
        => GetProjectionAsync<Query.GetPaymentDetails, Projection.Payment>(new(paymentId), cancellationToken);
}