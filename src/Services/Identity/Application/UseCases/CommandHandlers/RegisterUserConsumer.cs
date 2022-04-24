using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class RegisterUserConsumer : IConsumer<Command.RegisterUser>
{
    private readonly IUserEventStoreService _eventStoreService;

    public RegisterUserConsumer(IUserEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.RegisterUser> context)
    {
        var user = new User();

        user.Register(
            context.Message.Email,
            context.Message.FirstName,
            context.Message.Password,
            context.Message.PasswordConfirmation);

        await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
    }
}