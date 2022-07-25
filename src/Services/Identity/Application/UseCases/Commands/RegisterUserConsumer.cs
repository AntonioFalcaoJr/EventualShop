using Application.EventStore;
using Contracts.Services.Identity;
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
        User user = new();
        user.Handle(context.Message);
        await _eventStore.AppendAsync(user, context.CancellationToken);
    }
}