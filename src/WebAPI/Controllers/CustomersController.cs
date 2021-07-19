using System.Threading.Tasks;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.RegisterCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers
{
    public class CustomersController : ApplicationController
    {
        public CustomersController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterCustomerCommand command)
            => await SendCommand(command);

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCustomerCommand command)
            => await SendCommand(command);

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCustomerCommand command)
            => await SendCommand(command);
    }
}