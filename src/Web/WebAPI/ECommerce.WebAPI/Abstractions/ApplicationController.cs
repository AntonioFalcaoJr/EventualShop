using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Abstractions.Commands;
using ECommerce.Abstractions.Queries;
using ECommerce.Abstractions.Queries.Responses;
using MassTransit;
using MassTransit.Definition;
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
        where TCommand : class, ICommand
    {
        var endpoint = await _bus.GetSendEndpoint(Address<TCommand>());
        await endpoint.Send(command, cancellationToken);
        return Accepted();
    }

    protected async Task<IActionResult> GetResponseAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken)
        where TQuery : class, IQuery
        where TResponse : class, IResponse
    {
        var response = await _bus.CreateRequestClient<TQuery>(Address<TQuery>()).GetResponse<TResponse>(query, cancellationToken);
        return Ok(response.Message);
    }

    private static Uri Address<T>()
        => new($"exchange:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
}