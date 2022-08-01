using Application.Abstractions.UseCases;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RegisterUserConsumer : Consumer<Command.RegisterUser>
{
    public RegisterUserConsumer(IInteractor<Command.RegisterUser> interactor)
        : base(interactor) { }
}