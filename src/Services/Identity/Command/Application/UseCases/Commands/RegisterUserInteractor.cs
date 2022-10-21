using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class RegisterUserInteractor : IInteractor<Command.RegisterUser>
{
    private readonly IApplicationService _applicationService;

    public RegisterUserInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.RegisterUser message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.UserId, cancellationToken);
        aggregate.Handle(message);
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}