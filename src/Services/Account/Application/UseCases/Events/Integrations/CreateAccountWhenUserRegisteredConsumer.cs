using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Identities;
using MassTransit;
using Command = ECommerce.Contracts.Accounts.Command;

namespace Application.UseCases.Events.Integrations;

public class CreateAccountWhenUserRegisteredConsumer : IConsumer<DomainEvent.UserRegistered>
{
    private readonly IAccountEventStoreService _eventStore;

    public CreateAccountWhenUserRegisteredConsumer(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
    {
        var account = new Account();
        account.Handle(new Command.CreateAccount(context.Message.UserId, context.Message.Email));
        await _eventStore.AppendEventsAsync(account, context.CancellationToken);
    }
}