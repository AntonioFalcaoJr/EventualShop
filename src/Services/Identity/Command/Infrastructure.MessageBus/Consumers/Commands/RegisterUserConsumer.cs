using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RegisterUserConsumer : Consumer<Command.SignUp>
{
    public RegisterUserConsumer(IInteractor<Command.SignUp> interactor)
        : base(interactor) { }
}