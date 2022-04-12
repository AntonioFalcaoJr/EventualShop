using Application.EventSourcing.EventStore;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class DefineResidenceAddressConsumer : IConsumer<Commands.DefineResidenceAddress>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public DefineResidenceAddressConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.DefineResidenceAddress> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}