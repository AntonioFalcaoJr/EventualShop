using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using AccountCreatedEvent = Messages.Services.Accounts.DomainEvents.AccountCreated;

namespace Application.UseCases.Events.Projections;

public class AccountCreatedConsumer : IConsumer<AccountCreatedEvent>
{
    private readonly IAccountEventStoreService _eventStoreService;
    private readonly IAccountProjectionsService _projectionsService;

    public AccountCreatedConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

        var accountDetails = new AccountDetailsProjection
        {
            Id = account.Id,
            UserId = account.UserId,
            Profile = new()
            {
                Email = account.Profile.Email,
                FirstName = account.Profile.FirstName
            },
            IsDeleted = account.IsDeleted
        };

        await _projectionsService.ProjectNewAsync(accountDetails, context.CancellationToken);
    }
}