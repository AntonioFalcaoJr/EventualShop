using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers;

public class ProjectCatalogItemsWhenChangedConsumer :
    IConsumer<DomainEvents.CatalogDeleted>,
    IConsumer<DomainEvents.CatalogItemAdded>,
    IConsumer<DomainEvents.CatalogItemRemoved>,
    IConsumer<DomainEvents.CatalogItemUpdated>
{
    private readonly IProjectionsRepository<Projections.CatalogItem> _projectionsRepository;

    public ProjectCatalogItemsWhenChangedConsumer(IProjectionsRepository<Projections.CatalogItem> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => _projectionsRepository.DeleteAsync(item => item.CatalogId == context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemRemoved> context)
        => _projectionsRepository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemAdded> context)
    {
        Projections.CatalogItem catalogItem = new(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false);

        return _projectionsRepository.InsertAsync(catalogItem, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemUpdated> context)
    {
        Projections.CatalogItem catalogItem = new(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false);

        return _projectionsRepository.UpsertAsync(catalogItem, context.CancellationToken);
    }
}