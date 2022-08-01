using Application.Abstractions.UseCases;
using Application.EventStore;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases;

public class RegisterUserInteractor : IInteractor<Command.RegisterUser>
{
    private readonly IUserEventStoreService _eventStore;

    public RegisterUserInteractor(IUserEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task InteractAsync(Command.RegisterUser command, CancellationToken ct)
    {
        User user = new();
        user.Handle(command);
        await _eventStore.AppendAsync(user, ct);
    }
}