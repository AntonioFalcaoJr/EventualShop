using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.Commands;

public class CreateAccountConsumer : IConsumer<Command.CreateAccount>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public CreateAccountConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Command.CreateAccount> context)
    {
        var account = new Account();
        account.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}