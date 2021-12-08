using System.Threading;
using System.Threading.Tasks;
using ECommerce.Abstractions;
using ECommerce.Abstractions.Commands;
using ECommerce.Abstractions.Queries;
using ECommerce.Abstractions.Queries.Responses;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Abstractions;

[ApiController]
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

    private Task<Response<TResponse>> GetResponseAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : class, IQuery
        where TResponse : class, IResponse
        => _bus.CreateRequestClient<TRequest>().GetResponse<TResponse>(request, cancellationToken);

    private Task SendMessage<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : IMessage
        => _bus.Send(message, cancellationToken);
}