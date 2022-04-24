using Application.EventStore;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeUserPasswordConsumer : IConsumer<Command.ChangeUserPassword>
{
    private readonly IUserEventStoreService _eventStoreService;

    public ChangeUserPasswordConsumer(IUserEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.ChangeUserPassword> context)
    {
        var user = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.UserId, context.CancellationToken);

        user.ChangePassword(
            user.Id,
            context.Message.NewPassword,
            context.Message.NewPasswordConfirmation);

        await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
    }
}