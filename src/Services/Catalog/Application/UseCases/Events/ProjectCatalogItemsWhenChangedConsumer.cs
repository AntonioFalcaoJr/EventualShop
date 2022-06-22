using Application.Abstractions.Projections;
using Contracts.Services.Catalog;
using MassTransit;

namespace Application.UseCases.Events;

public class ProjectCatalogItemsWhenChangedConsumer :
    IConsumer<DomainEvent.CatalogDeleted>,
    IConsumer<DomainEvent.CatalogItemAdded>,
    IConsumer<DomainEvent.CatalogItemRemoved>
{
    private readonly IProjectionRepository<Projection.CatalogItem> _repository;

    public ProjectCatalogItemsWhenChangedConsumer(IProjectionRepository<Projection.CatalogItem> repository)
        => _repository = repository;

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => _repository.DeleteAsync(item => item.CatalogId == context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemRemoved> context)
        => _repository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
    {
        Projection.CatalogItem catalogItem = new(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.InventoryId,
            context.Message.Product,
            false);

        return _repository.InsertAsync(catalogItem, context.CancellationToken);
    }
}