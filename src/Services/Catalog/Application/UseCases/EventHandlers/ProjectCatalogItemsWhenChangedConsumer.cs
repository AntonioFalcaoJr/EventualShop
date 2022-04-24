using Application.Abstractions.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers;

public class ProjectCatalogItemsWhenChangedConsumer :
    IConsumer<DomainEvent.CatalogDeleted>,
    IConsumer<DomainEvent.CatalogItemAdded>,
    IConsumer<DomainEvent.CatalogItemRemoved>,
    IConsumer<DomainEvent.CatalogItemUpdated>
{
    private readonly IProjectionRepository<Projection.CatalogItem> _projectionRepository;

    public ProjectCatalogItemsWhenChangedConsumer(IProjectionRepository<Projection.CatalogItem> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => await _projectionRepository.DeleteAsync(item => item.CatalogId == context.Message.CatalogId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CatalogItemRemoved> context)
        => await _projectionRepository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
    {
        Projection.CatalogItem catalogItem = new(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false);

        await _projectionRepository.InsertAsync(catalogItem, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.CatalogItemUpdated> context)
    {
        Projection.CatalogItem catalogItem = new(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false);

        await _projectionRepository.UpsertAsync(catalogItem, context.CancellationToken);
    }
}