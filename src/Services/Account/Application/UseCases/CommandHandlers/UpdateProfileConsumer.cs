using Application.EventStore;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class UpdateProfileConsumer : IConsumer<Commands.UpdateProfile>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public UpdateProfileConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.UpdateProfile> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}