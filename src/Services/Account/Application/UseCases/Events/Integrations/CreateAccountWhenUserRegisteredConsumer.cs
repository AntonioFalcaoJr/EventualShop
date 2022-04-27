using Application.EventStore;
using Contracts.Services.Identity;
using Domain.Aggregates;
using MassTransit;
using Command = Contracts.Services.Account.Command;

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