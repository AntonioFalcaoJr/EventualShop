using System.Threading.Tasks;
using Application.Customers.Commands.Delete;
using Application.Customers.Commands.Register;
using Application.Customers.Commands.Update;
using MediatR;
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