using Application.EventStore;
using Contracts.Services.Account;
using Domain.Aggregates;
using MassTransit;

namespace Application.UseCases.Commands;

public class CreateAccountConsumer : IConsumer<Command.CreateAccount>
{
    private readonly IAccountEventStoreService _eventStore;

    public CreateAccountConsumer(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.CreateAccount> context)
    {
        var account = new Account();
        account.Handle(context.Message);
        await _eventStore.AppendEventsAsync(account, context.CancellationToken);
    }
}