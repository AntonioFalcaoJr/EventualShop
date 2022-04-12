using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Accounts;
using MassTransit;
using DomainEvents = ECommerce.Contracts.Identity.DomainEvents;

namespace Application.UseCases.EventHandlers.Integrations;

public class CreateAccountWhenUserRegisteredConsumer : IConsumer<DomainEvents.UserRegistered>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public CreateAccountWhenUserRegisteredConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvents.UserRegistered> context)
    {
        var account = new Account();

        account.Handle(
            new Commands.CreateAccount(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName));

        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}