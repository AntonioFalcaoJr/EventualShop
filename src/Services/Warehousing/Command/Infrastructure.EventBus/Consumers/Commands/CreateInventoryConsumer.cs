using Application.Abstractions;
using Contracts.Boundaries.Warehouse;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class CreateInventoryConsumer(IInteractor<Command.CreateInventory> interactor) : Consumer<Command.CreateInventory>(interactor);