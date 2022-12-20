using Application.Abstractions;
using Application.Services;
using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using Domain.Aggregates;
using MassTransit;

namespace Application.UseCases.Events;

public class RebuildProjectionRequestedInteractor : IInteractor<NotificationEvent.RebuildProjectionRequested>
{
    private readonly IApplicationService _applicationService;
    private readonly IBus _bus;

    public RebuildProjectionRequestedInteractor(IApplicationService applicationService, IBus bus)
    {
        _applicationService = applicationService;
        _bus = bus;
    }

    public async Task InteractAsync(NotificationEvent.RebuildProjectionRequested @event, CancellationToken cancellationToken)
    {
        var account = await _applicationService.LoadAggregateAsync<Account>(@event.AccountId, cancellationToken);

        IntegrationEvent.ProjectionRebuilt integrationEvent = new(@event.AccountId, account);

        var exchange = $"exchange:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(nameof(Account))}" +
                       $".{KebabCaseEndpointNameFormatter.Instance.SanitizeName(@event.Name)}" +
                       $".{KebabCaseEndpointNameFormatter.Instance.SanitizeName(nameof(IntegrationEvent.ProjectionRebuilt))}";

        var endpoint = await _bus.GetSendEndpoint(new(exchange));
        await endpoint.Send(@event, cancellationToken);
    }
}