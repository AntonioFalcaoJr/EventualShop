using Application.Abstractions.UseCases;
using Application.EventStore;
using Contracts.Services.Identity;

namespace Application.UseCases;

public class ChangePasswordInteractor : IInteractor<Command.ChangePassword>
{
    private readonly IUserEventStoreService _eventStore;

    public ChangePasswordInteractor(IUserEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task InteractAsync(Command.ChangePassword command, CancellationToken ct)
    {
        var user = await _eventStore.LoadAsync(command.UserId, ct);
        user.Handle(command);
        await _eventStore.AppendAsync(user, ct);
    }
}