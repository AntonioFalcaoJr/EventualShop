using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using Infrastructure.Authentication.Abstractions;

namespace Application.UseCases.Commands;

public class RegisterUserInteractor : IInteractor<Command.RegisterUser>
{
    private readonly IApplicationService _applicationService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserInteractor(
        IApplicationService applicationService,
        IPasswordHasher passwordHasher)
    {
        _applicationService = applicationService;
        _passwordHasher = passwordHasher;
    }

    public async Task InteractAsync(Command.RegisterUser command, CancellationToken cancellationToken)
    {
        var message = command with { Password = _passwordHasher.HashPassword(command.Password) };

        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.UserId, cancellationToken);
        aggregate.Handle(message);
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}