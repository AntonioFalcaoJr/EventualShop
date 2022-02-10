using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Abstractions.Messages.Commands;
using ECommerce.Abstractions.Messages.Queries;
using ECommerce.Abstractions.Messages.Queries.Responses;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Abstractions;

[ApiController]
public abstract class ApplicationController : ControllerBase
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;

    protected ApplicationController(IBus bus, IMapper mapper)
    {
        _bus = bus;
        _mapper = mapper;
    }

    protected async Task<IActionResult> SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : class, ICommand
    {
        var endpoint = await _bus.GetSendEndpoint(Address<TCommand>());
        await endpoint.Send(command, cancellationToken);
        return Accepted();
    }

    protected async Task<IActionResult> GetResponseAsync<TQuery, TResponse, TNotFound, TOutput>(TQuery query, CancellationToken cancellationToken)
        where TQuery : class, IQuery
        where TResponse : class, IResponse
        where TNotFound : class, IResponse
    {
        var clientResponse = await _bus
            .CreateRequestClient<TQuery>(Address<TQuery>())
            .GetResponse<TResponse, TNotFound>(query, cancellationToken);

        return clientResponse.Message switch
        {
            TResponse response => Ok(_mapper.Map<TOutput>(response)),
            TNotFound notFound => NotFound(notFound),
            _ => Problem()
        };
    }

    private static Uri Address<T>()
        => new($"exchange:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
}