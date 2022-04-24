using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.Commands;

public class RegisterUserConsumer : IConsumer<Command.RegisterUser>
{
    private readonly IUserEventStoreService _eventStore;

    public RegisterUserConsumer(IUserEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.RegisterUser> context)
    {
        var user = new User();

        user.Register(
            context.Message.Email,
            context.Message.FirstName,
            context.Message.Password,
            context.Message.PasswordConfirmation);

        await _eventStore.AppendEventsAsync(user, context.CancellationToken);
    }
}