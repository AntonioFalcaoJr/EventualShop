using Application.Abstractions;
using Contracts.Boundaries.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class DeleteAccountConsumer(IInteractor<Command.DeleteAccount> interactor) : Consumer<Command.DeleteAccount>(interactor);