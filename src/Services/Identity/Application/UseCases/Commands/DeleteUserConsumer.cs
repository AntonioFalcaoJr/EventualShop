using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using DeleteUserCommand = Messages.Services.Identities.Commands.DeleteUser;

namespace Application.UseCases.Commands;

public class DeleteUserConsumer : IConsumer<DeleteUserCommand>
{
    private readonly IUserEventStoreService _eventStoreService;

    public DeleteUserConsumer(IUserEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DeleteUserCommand> context)
    {
        var user = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.UserId, context.CancellationToken);
        user.Delete(user.Id);
        await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
    }
}