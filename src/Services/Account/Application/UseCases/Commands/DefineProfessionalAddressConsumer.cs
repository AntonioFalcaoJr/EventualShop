using Application.EventStore;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.Commands;

public class DefineProfessionalAddressConsumer : IConsumer<Command.DefineProfessionalAddress>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public DefineProfessionalAddressConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.DefineProfessionalAddress> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}