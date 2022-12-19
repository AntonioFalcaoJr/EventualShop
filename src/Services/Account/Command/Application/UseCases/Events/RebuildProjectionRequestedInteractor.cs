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

    public RebuildProjectionRequestedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(NotificationEvent.RebuildProjectionRequested @event, CancellationToken cancellationToken)
    {
        var account = (Account)await _applicationService.LoadAggregateAsync<Account>(@event.AccountId, cancellationToken);

        IntegrationEvent.ProjectionRebuilt integrationEvent = new(account.Id, account.Profile, 
            account.Addresses.ToDictionary(key => key.Id, value => (Dto.Address)value), account.WishToReceiveNews, account.AcceptedPolicies, account.IsDeleted);

        await _applicationService.RebuildAsync<Account>(integrationEvent, @event.Name, cancellationToken);
    }
}