using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class RegisterUserConsumer(IInteractor<Command.RegisterUser> interactor) : Consumer<Command.RegisterUser>(interactor);