using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class RegisterUserInteractor(IApplicationService service) : IInteractor<Command.RegisterUser>
{
    public async Task InteractAsync(Command.RegisterUser command, CancellationToken cancellationToken)
    {
        var user = await service.LoadAggregateAsync<User>(command.UserId, cancellationToken);
        user.Handle(command);
        await service.AppendEventsAsync(user, cancellationToken);
    }
}