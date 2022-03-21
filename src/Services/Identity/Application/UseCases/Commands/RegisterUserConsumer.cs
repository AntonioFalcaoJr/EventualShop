using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using RegisterUserCommand = ECommerce.Contracts.Identity.Commands.RegisterUser;

namespace Application.UseCases.Commands;

public class RegisterUserConsumer : IConsumer<RegisterUserCommand>
{
    private readonly IUserEventStoreService _eventStoreService;

    public RegisterUserConsumer(IUserEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<RegisterUserCommand> context)
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