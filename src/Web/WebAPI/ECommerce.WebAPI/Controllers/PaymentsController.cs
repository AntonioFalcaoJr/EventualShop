using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Messages.Payments;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    public class PaymentsController : ApplicationController
    {
        public PaymentsController(IBus bus)
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetCart([FromQuery] Queries.GetPaymentDetails query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<Queries.GetPaymentDetails, Responses.PaymentDetails>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RequestPayment(Commands.RequestPayment command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> CancelPayment(Commands.CancelPayment command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}