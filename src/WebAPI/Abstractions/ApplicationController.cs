using System.Threading.Tasks;
using MediatR;
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

        protected async Task<IActionResult> SendCommand(IRequest command)
        {
            await _mediator.Send(command);
            return Accepted();
        }
    }
}