using Application.EventStore;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.Commands;

public class DeleteUserConsumer : IConsumer<Command.DeleteUser>
{
    private readonly IUserEventStoreService _eventStore;

    public DeleteUserConsumer(IUserEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.DeleteUser> context)
    {
        var user = await _eventStore.LoadAggregateAsync(context.Message.UserId, context.CancellationToken);
        user.Delete(user.Id);
        await _eventStore.AppendEventsAsync(user, context.CancellationToken);
    }
}