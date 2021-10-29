using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using CreateAccountCommand = Messages.Accounts.Commands.CreateAccount;

namespace Application.UseCases.Commands;

public class CreateAccountConsumer : IConsumer<CreateAccountCommand>
{
    private readonly IAccountEventStoreService _eventStoreService;

    public CreateAccountConsumer(IAccountEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<CreateAccountCommand> context)
    {
        var account = new Account();

        account.Create(
            context.Message.UserId,
            context.Message.Email,
            context.Message.FirstName);

        await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
    }
}