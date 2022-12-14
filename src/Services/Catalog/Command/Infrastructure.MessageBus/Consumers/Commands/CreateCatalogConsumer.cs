using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class CreateCatalogConsumer : Consumer<Command.CreateCatalog>
{
    public CreateCatalogConsumer(ICreateCatalogInteractor interactor)
        : base(interactor) { }
}