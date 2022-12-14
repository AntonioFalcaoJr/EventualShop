using Application.Abstractions;
using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ActivateCatalogConsumer : Consumer<Command.ActivateCatalog>
{
    public ActivateCatalogConsumer(IActivateCatalogInteractor interactor)
        : base(interactor) { }
}