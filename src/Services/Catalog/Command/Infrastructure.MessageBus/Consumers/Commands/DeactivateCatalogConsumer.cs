using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DeactivateCatalogConsumer : Consumer<Command.DeactivateCatalog>
{
    public DeactivateCatalogConsumer(IDeactivateCatalogInteractor interactor)
        : base(interactor) { }
}