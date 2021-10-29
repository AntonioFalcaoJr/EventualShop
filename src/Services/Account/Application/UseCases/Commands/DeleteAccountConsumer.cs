using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using DeleteAccountCommand = Messages.Accounts.Commands.DeleteAccount;

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
        account.Delete(account.Id);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}