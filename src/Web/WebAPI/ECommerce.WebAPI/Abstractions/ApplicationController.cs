using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Messages.Abstractions;
using Messages.Abstractions.Commands;
using Messages.Abstractions.Queries;
using Messages.Abstractions.Queries.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Abstractions;

[ApiController, Route("api/[controller]/[action]")]
public abstract class ApplicationController : ControllerBase
{
    private readonly IBus _bus;

    protected ApplicationController(IBus bus)
    {
        _bus = bus;
    }

    protected async Task<IActionResult> SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand
    {
        await SendMessage(command, cancellationToken);
        return Accepted();
    }

    protected async Task<IActionResult> GetQueryResponseAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken)
        where TQuery : class, IQuery
        where TResponse : class, IResponse
    {
        var response = await GetResponseAsync<TQuery, TResponse>(query, cancellationToken);
        return Ok(response.Message);
    }

    private Task<Response<TResponse>> GetResponseAsync<TMessage, TResponse>(TMessage message, CancellationToken cancellationToken)
        where TMessage : class, IMessage
        where TResponse : class, IResponse
        => _bus.CreateRequestClient<TMessage>().GetResponse<TResponse>(message, cancellationToken);

    private Task SendMessage<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : IMessage
        => _bus.Send(message, cancellationToken);
}