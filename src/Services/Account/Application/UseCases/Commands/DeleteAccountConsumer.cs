using Application.EventStore;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.Commands;

public class DeleteAccountConsumer : IConsumer<Command.DeleteAccount>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public DeleteAccountConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.DeleteAccount> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}