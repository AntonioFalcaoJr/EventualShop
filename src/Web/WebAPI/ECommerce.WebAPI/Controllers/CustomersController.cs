using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using ECommerce.WebAPI.Messages.Commands;
using ECommerce.WebAPI.Messages.Queries;
using MassTransit;
using Messages.Customers.Queries;
using Messages.Customers.Queries.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    public class CustomersController : ApplicationController
    {
        public CustomersController(IBus bus)
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetCustomersWithPagination([FromQuery] Queries.GetCustomersDetailsWithPaginationQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomersDetailsWithPagination, CustomersDetailsPagedResult>(query, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetCustomerDetails([FromQuery] Queries.GetCustomerDetailsQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomerDetails, CustomerDetails>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RegisterCustomer(Commands.RegisterCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> UpdateCustomer(Commands.UpdateCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteCustomer(Commands.DeleteCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}