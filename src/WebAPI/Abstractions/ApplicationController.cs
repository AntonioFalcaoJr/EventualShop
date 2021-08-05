using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.UseCases;
using Application.Abstractions.UseCases.Models;
using MassTransit;
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

        protected async Task<IActionResult> SendCommandAsync(ICommand command, CancellationToken cancellationToken)
        {
            await SendMessage(command, cancellationToken);
            return Accepted();
        }

        protected async Task<IActionResult> SendQueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken)
            where TQuery : class, IQuery
            where TResponse : Model
        {
            await SendMessage(query, cancellationToken);
            var response = await GetRequestClient<TQuery>().GetResponse<TResponse>(query, cancellationToken);
            return Ok(response.Message);
        }

        private IRequestClient<T> GetRequestClient<T>() where T : class
            => _mediator.CreateRequestClient<T>();

        private Task SendMessage<TMessage>(TMessage message, CancellationToken cancellationToken)
            => _mediator.Send(message, cancellationToken);
    }
}