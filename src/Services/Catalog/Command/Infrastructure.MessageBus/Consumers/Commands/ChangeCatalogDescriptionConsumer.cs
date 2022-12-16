using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ChangeCatalogDescriptionConsumer : Consumer<Command.ChangeCatalogDescription>
{
    public ChangeCatalogDescriptionConsumer(IChangeCatalogDescriptionInteractor interactor)
        : base(interactor) { }
}