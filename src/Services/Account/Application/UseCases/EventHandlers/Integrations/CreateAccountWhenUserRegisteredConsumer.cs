using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Identities;
using MassTransit;
using Command = ECommerce.Contracts.Accounts.Command;

namespace Application.UseCases.EventHandlers.Integrations;

public class CreateAccountWhenUserRegisteredConsumer : IConsumer<DomainEvent.UserRegistered>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public CreateAccountWhenUserRegisteredConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
    {
        var account = new Account();

        account.Handle(
            new Command.CreateAccount(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName));

        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}