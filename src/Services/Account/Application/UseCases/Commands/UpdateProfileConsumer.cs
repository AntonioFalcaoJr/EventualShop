using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using UpdateProfileCommand = ECommerce.Contracts.Account.Commands.UpdateProfile;

namespace Application.UseCases.Commands;

public class UpdateProfileConsumer : IConsumer<UpdateProfileCommand>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public UpdateProfileConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<UpdateProfileCommand> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

        if (account is null)
            // TODO - Notification
            return;

        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}