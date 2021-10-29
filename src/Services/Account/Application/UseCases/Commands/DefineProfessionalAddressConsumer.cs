using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using DefineProfessionalAddressCommand = Messages.Accounts.Commands.DefineProfessionalAddress;

namespace Application.UseCases.Commands;

public class DefineProfessionalAddressConsumer : IConsumer<DefineProfessionalAddressCommand>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public DefineProfessionalAddressConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DefineProfessionalAddressCommand> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

        account.DefineProfessionalAddress(
            account.Id,
            context.Message.City,
            context.Message.Country,
            context.Message.Number,
            context.Message.State,
            context.Message.Street,
            context.Message.ZipCode);

        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}