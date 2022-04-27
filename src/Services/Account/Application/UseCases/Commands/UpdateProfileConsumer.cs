using Application.EventStore;
using Contracts.Services.Account;
using MassTransit;

namespace Application.UseCases.Commands;

public class UpdateProfileConsumer : IConsumer<Command.UpdateProfile>
{
    private readonly IAccountEventStoreService _eventStore;

    public UpdateProfileConsumer(IAccountEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.UpdateProfile> context)
    {
        var account = await _eventStore.LoadAggregateAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStore.AppendEventsAsync(account, context.CancellationToken);
    }
}