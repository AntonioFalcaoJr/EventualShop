using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using DeleteAccountCommand = ECommerce.Contracts.Account.Commands.DeleteAccount;

namespace Application.UseCases.Commands;

public class DeleteAccountConsumer : IConsumer<DeleteAccountCommand>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public DeleteAccountConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DeleteAccountCommand> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}