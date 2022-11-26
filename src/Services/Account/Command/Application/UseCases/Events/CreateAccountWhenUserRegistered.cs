using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using MassTransit;
using Command = Contracts.Services.Account.Command;

namespace Application.UseCases.Events;

public class CreateAccountWhenUserRegistered : IConsumer<DomainEvent.UserRegistered>
{
    private readonly IApplicationService _service;

    public CreateAccountWhenUserRegistered(IApplicationService service)
    {
        _service = service;
    }

    public async Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
    {
        Account account = new();

        var command = new Command.CreateAccount(
            context.Message.FirstName,
            context.Message.LastName,
            context.Message.Email);

        account.Handle(command);
        
        await _service.AppendEventsAsync(account, context.CancellationToken);
    }
}