using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ChangeCatalogDescriptionConsumer : Consumer<Command.ChangeCatalogDescription>
{
    public ChangeCatalogDescriptionConsumer(IInteractor<Command.ChangeCatalogDescription> interactor)
        : base(interactor) { }
}