using Application.Abstractions;
using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ChangeCatalogTitleConsumer : Consumer<Command.ChangeCatalogTitle>
{
    public ChangeCatalogTitleConsumer(IChangeCatalogTitleInteractor interactor)
        : base(interactor) { }
}