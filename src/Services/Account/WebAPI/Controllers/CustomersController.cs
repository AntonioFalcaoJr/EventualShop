using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Customers.Projections;
using MassTransit;
using Messages.Customers.Commands;
using Messages.Customers.Queries;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers
{
    public class CustomersController : ApplicationController
    {
        public CustomersController(IBus bus)
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetCustomersWithPagination([FromQuery] GetCustomersDetailsWithPagination query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomersDetailsWithPagination, IPagedResult<CustomerDetailsProjection>>(query, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetCustomerDetails([FromQuery] GetCustomerDetails query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomerDetails, CustomerDetailsProjection>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RegisterCustomer(RegisterCustomer command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> UpdateCustomer(UpdateCustomer command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteCustomer(DeleteCustomer command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}