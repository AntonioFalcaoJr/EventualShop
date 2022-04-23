using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class CreateAccountConsumer : IConsumer<Commands.CreateAccount>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public CreateAccountConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.CreateAccount> context)
    {
        var account = new Account();
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}