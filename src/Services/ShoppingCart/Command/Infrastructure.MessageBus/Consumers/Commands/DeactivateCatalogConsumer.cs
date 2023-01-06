using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DeactivateCatalogConsumer : Consumer<Command.DeactivateCatalog>
{
    public DeactivateCatalogConsumer(IInteractor<Command.DeactivateCatalog> interactor)
        : base(interactor) { }
}