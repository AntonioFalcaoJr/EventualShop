using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using UserRegisteredEvent = ECommerce.Contracts.Identity.DomainEvents.UserRegistered;
using CreateAccountCommand = ECommerce.Contracts.Account.Commands.CreateAccount;

namespace Application.UseCases.Events.Integrations;

public class CreateAccountWhenUserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public CreateAccountWhenUserRegisteredConsumer(IAccountEventStoreService eventStoreService)
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

        await _eventStoreService.AppendEventsToStreamAsync(account, context.Message, context.CancellationToken);
    }
}