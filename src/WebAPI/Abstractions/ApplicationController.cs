using System.Threading.Tasks;
using Application.Abstractions.Commands;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> SendCommand(ICommand command)
        {
            await _mediator.Send(command);
            return Accepted();
        }
    }
}