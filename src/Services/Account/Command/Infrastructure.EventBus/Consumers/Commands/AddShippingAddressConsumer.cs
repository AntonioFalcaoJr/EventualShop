using Application.Abstractions;
using Contracts.Boundaries.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class AddShippingAddressConsumer(IInteractor<Command.AddShippingAddress> interactor) : Consumer<Command.AddShippingAddress>(interactor);