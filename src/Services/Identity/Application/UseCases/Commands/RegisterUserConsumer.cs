using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.Commands;

public class RegisterUserConsumer : IConsumer<Command.Register>
{
    private readonly IUserEventStoreService _eventStore;

    public RegisterUserConsumer(IUserEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.Register> context)
    {
        var user = new User();
        user.Handle(context.Message);
        await _eventStore.AppendEventsAsync(user, context.CancellationToken);
    }
}