using Application.EventStore;
using Contracts.Services.Account;
using MassTransit;

namespace Application.UseCases.Commands;

public class DefineResidenceAddressConsumer : IConsumer<Command.AddBillingAddress>
{
    private readonly IAccountEventStoreService _eventStore;

    public DefineResidenceAddressConsumer(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.AddBillingAddress> context)
    {
        var account = await _eventStore.LoadAggregateAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStore.AppendEventsAsync(account, context.CancellationToken);
    }
}