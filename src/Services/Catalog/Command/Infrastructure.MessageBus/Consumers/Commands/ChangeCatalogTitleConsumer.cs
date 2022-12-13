using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ChangeCatalogTitleConsumer : Consumer<Command.ChangeCatalogTitle>
{
    public ChangeCatalogTitleConsumer(IInteractor<Command.ChangeCatalogTitle> interactor)
        : base(interactor) { }
}