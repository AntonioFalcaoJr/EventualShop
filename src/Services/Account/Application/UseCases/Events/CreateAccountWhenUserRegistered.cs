using Application.EventStore;
using Contracts.Services.Identity;
using Domain.Aggregates;
using MassTransit;
using Command = Contracts.Services.Account.Command;

namespace Application.UseCases.Events;

public class CreateAccountWhenUserRegistered : IConsumer<DomainEvent.UserRegistered>
{
    private readonly IAccountEventStoreService _eventStore;

    public CreateAccountWhenUserRegistered(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
    {
        Account account = new();

        var command = new Command.CreateAccount(
            Guid.NewGuid(),
            context.Message.FirstName,
            context.Message.LastName,
            context.Message.Email);

        account.Handle(command);
        
        await _eventStore.AppendAsync(account, context.CancellationToken);
    }
}