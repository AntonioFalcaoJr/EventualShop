using Application.EventStore;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.Commands;

public class DeleteAccountConsumer : IConsumer<Command.DeleteAccount>
{
    private readonly IAccountEventStoreService _eventStore;

    public DeleteAccountConsumer(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DeleteAccount> context)
    {
        var account = await _eventStore.LoadAggregateAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStore.AppendEventsAsync(account, context.CancellationToken);
    }
}