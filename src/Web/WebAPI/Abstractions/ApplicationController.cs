using Contracts.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Abstractions;

[ApiController, Route("api/v1/[controller]")]
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

    protected async Task<IActionResult> GetProjectionAsync<TQuery, TProjection>(TQuery query, CancellationToken cancellationToken)
        where TQuery : class, IQuery
        where TProjection : class
    {
        var response = await _bus
            .CreateRequestClient<TQuery>(Address<TQuery>())
            .GetResponse<TProjection, NoContent, NotFound>(query, cancellationToken);

        return response.Message switch
        {
            TProjection projection => Ok(projection),
            NoContent _ => NoContent(),
            NotFound _ => NotFound(),
            _ => Problem()
        };
    }

    private static Uri Address<T>()
        => new($"exchange:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
}