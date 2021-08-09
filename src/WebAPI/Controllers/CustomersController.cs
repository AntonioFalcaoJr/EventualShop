using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.RegisterCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.Queries.GetCustomerDetails;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
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
        public Task<IActionResult> GetCustomersWithPagination([FromQuery] GetCustomersDetailsWithPaginationQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomersDetailsWithPaginationQuery, IPagedResult<CustomerDetailsProjection>>(query, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetCustomerDetails([FromQuery] GetCustomerDetailsQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCustomerDetailsQuery, CustomerDetailsProjection>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RegisterCustomer(RegisterCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> UpdateCustomer(UpdateCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteCustomer(DeleteCustomerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}