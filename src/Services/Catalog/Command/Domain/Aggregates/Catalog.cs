using Domain.Abstractions.Aggregates;
using Domain.Entities.CatalogItems;
using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class Catalog : AggregateRoot<CatalogValidator>
{
    [JsonProperty]
    private readonly List<CatalogItem> _items = new();

    public bool IsActive { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }

    public IEnumerable<CatalogItem> Items
        => _items.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    public void Handle(Command.CreateCatalog cmd)
        => RaiseEvent<DomainEvent.CatalogCreated>(version => new(cmd.CatalogId, cmd.Title, cmd.Description, version));

    private void Handle(Command.DeleteCatalog cmd)
        => RaiseEvent<DomainEvent.CatalogDeleted>(version => new(cmd.CatalogId, version));

    private void Handle(Command.ActivateCatalog cmd)
    {
        if (Items.Any() && IsActive is false)
            RaiseEvent<DomainEvent.CatalogActivated>(version => new(cmd.CatalogId, version));
    }

    private void Handle(Command.DeactivateCatalog cmd)
    {
        if (IsActive)
            RaiseEvent<DomainEvent.CatalogDeactivated>(version => new(cmd.CatalogId, version));
    }

    private void Handle(Command.ChangeCatalogDescription cmd)
        => RaiseEvent<DomainEvent.CatalogDescriptionChanged>(version => new(cmd.CatalogId, cmd.Description, version));

    private void Handle(Command.ChangeCatalogTitle cmd)
        => RaiseEvent<DomainEvent.CatalogTitleChanged>(version => new(cmd.CatalogId, cmd.Title, version));

    private void Handle(Command.AddCatalogItem cmd)
    {
        var item = _items
            .Where(catalogItem => catalogItem.Product == cmd.Product)
            .SingleOrDefault(catalogItem => catalogItem.UnitPrice == cmd.UnitPrice);

        RaiseEvent(version => item is { IsDeleted: false }
            ? new DomainEvent.CatalogItemIncreased(cmd.CatalogId, item.Id, cmd.InventoryId, cmd.Quantity, version)
            : new DomainEvent.CatalogItemAdded(cmd.CatalogId, Guid.NewGuid(), cmd.InventoryId, cmd.Product, cmd.UnitPrice, cmd.Sku, cmd.Quantity, version));
    }

    private void Handle(Command.RemoveCatalogItem cmd)
        => RaiseEvent<DomainEvent.CatalogItemRemoved>(version => new(cmd.CatalogId, cmd.ItemId, version));

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.CatalogCreated @event)
        => (Id, Title, Description, _) = @event;

    private void When(DomainEvent.CatalogDescriptionChanged @event)
        => Description = @event.Description;

    private void When(DomainEvent.CatalogTitleChanged @event)
        => Title = @event.Title;

    private void When(DomainEvent.CatalogDeleted _)
        => IsDeleted = true;

    private void When(DomainEvent.CatalogActivated _)
        => IsActive = true;

    private void When(DomainEvent.CatalogDeactivated _)
        => IsActive = false;

    private void When(DomainEvent.CatalogItemAdded @event)
        => _items.Add(new(@event.ItemId, @event.InventoryId, @event.Product, @event.UnitPrice, @event.Sku, @event.Quantity));

    private void When(DomainEvent.CatalogItemIncreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Increase(@event.Quantity);

    private void When(DomainEvent.CatalogItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);
}