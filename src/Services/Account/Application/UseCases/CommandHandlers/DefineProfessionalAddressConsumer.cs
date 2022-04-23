using Application.EventStore;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class DefineProfessionalAddressConsumer : IConsumer<Commands.DefineProfessionalAddress>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public DefineProfessionalAddressConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.DefineProfessionalAddress> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}