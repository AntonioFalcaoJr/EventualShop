using Application.Abstractions;
using Contracts.Boundaries.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class AddBillingAddressConsumer(IInteractor<Command.AddBillingAddress> interactor) : Consumer<Command.AddBillingAddress>(interactor);