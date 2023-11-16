using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class ChangePasswordInteractor(IApplicationService service) : IInteractor<Command.ChangePassword>
{
    public async Task InteractAsync(Command.ChangePassword command, CancellationToken cancellationToken)
    {
        var user = await service.LoadAggregateAsync<User>(command.UserId, cancellationToken);
        user.Handle(command);
        await service.AppendEventsAsync(user, cancellationToken);
    }
}