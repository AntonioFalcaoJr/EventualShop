using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using ChangeUserPasswordCommand = Messages.Identities.Commands.ChangeUserPassword;

namespace Application.UseCases.Commands;

public class ChangeUserPasswordConsumer : IConsumer<ChangeUserPasswordCommand>
{
    private readonly IUserEventStoreService _eventStoreService;

    public ChangeUserPasswordConsumer(IUserEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<ChangeUserPasswordCommand> context)
    {
        var user = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.UserId, context.CancellationToken);

        if (user is null)
            // TODO - Notification
            return;

        user.ChangePassword(
            user.Id,
            context.Message.NewPassword,
            context.Message.NewPasswordConfirmation);

        await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
    }
}