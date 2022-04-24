using Application.EventStore;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.Commands;

public class DeleteUserConsumer : IConsumer<Command.DeleteUser>
{
    private readonly IUserEventStoreService _eventStoreService;

    public DeleteUserConsumer(IUserEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.DeleteUser> context)
    {
        var user = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.UserId, context.CancellationToken);
        user.Delete(user.Id);
        await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
    }
}