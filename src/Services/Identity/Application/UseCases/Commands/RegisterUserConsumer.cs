using Application.EventStore;
using Contracts.Services.Identities;
using Domain.Aggregates;
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