using Application.EventStore;
using Contracts.Services.Identities;
using MassTransit;

namespace Application.UseCases.Commands;

public class ChangeUserPasswordConsumer : IConsumer<Command.ChangePassword>
{
    private readonly IUserEventStoreService _eventStore;

    public ChangeUserPasswordConsumer(IUserEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.ChangePassword> context)
    {
        var user = await _eventStore.LoadAggregateAsync(context.Message.UserId, context.CancellationToken);
        user.Handle(context.Message);
        await _eventStore.AppendEventsAsync(user, context.CancellationToken);
    }
}