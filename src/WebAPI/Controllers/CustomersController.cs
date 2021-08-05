using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.RegisterCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.Queries.CustomerDetails;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers
{
    public class CustomersController : ApplicationController
    {
        public CustomersController(IMediator mediator)
            : base(mediator) { }

        [HttpGet]
        public Task<IActionResult> Get([FromQuery] CustomerDetailsQuery query, CancellationToken cancellationToken)
            => SendQueryAsync<CustomerDetailsQuery, CustomerDetailsModel>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> Post(RegisterCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> Put(UpdateCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> Delete(DeleteCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}