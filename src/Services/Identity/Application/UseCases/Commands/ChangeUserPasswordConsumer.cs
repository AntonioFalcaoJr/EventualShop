using Application.EventStore;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeUserPasswordConsumer : IConsumer<Command.ChangeUserPassword>
{
    private readonly IUserEventStoreService _eventStore;

    public ChangeUserPasswordConsumer(IUserEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ChangeUserPassword> context)
    {
        var user = await _eventStore.LoadAggregateAsync(context.Message.UserId, context.CancellationToken);

        user.ChangePassword(
            user.Id,
            context.Message.NewPassword,
            context.Message.NewPasswordConfirmation);

        await _eventStore.AppendEventsAsync(user, context.CancellationToken);
    }
}