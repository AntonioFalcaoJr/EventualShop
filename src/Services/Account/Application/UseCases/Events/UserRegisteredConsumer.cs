using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using UserRegisteredEvent = Messages.Services.Identities.DomainEvents.UserRegistered;
using CreateAccountCommand = Messages.Services.Accounts.Commands.CreateAccount;

namespace Application.UseCases.Events;

public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public UserRegisteredConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var account = new Account();

        account.Handle(
            new CreateAccountCommand(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName));

        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}