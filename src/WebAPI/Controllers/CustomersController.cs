using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.RegisterCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.Queries.GetCustomerDetails;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers
{
    public class CustomersController : ApplicationController
    {
        public CustomersController(IBus bus)
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetCustomersWithPagination([FromQuery] GetCustomersDetailsWithPaginationQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomersDetailsWithPaginationQuery, IPagedResult<CustomerDetailsProjection>>(query, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetCustomerDetails([FromQuery] GetCustomerDetailsQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomerDetailsQuery, CustomerDetailsProjection>(query, cancellationToken);

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