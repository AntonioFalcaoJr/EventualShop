using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Enumerations;
using Domain.ValueObjects;
using static Contracts.Boundaries.Cataloging.Catalog.DomainEvent;
using static Domain.Exceptions;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.Catalogs;

public class Catalog : AggregateRoot<CatalogId>
{
    public AppId AppId { get; private set; } = AppId.Undefined;
    public CatalogStatus Status { get; private set; } = CatalogStatus.Undefined;
    public Title Title { get; private set; } = Title.Undefined;
    public Description Description { get; private set; } = Description.Undefined;

    public void Register(AppId appId, Title title, Description description)
    {
        CatalogAlreadyCreated.ThrowIf(Status is not Undefined);
        RaiseEvent<CatalogRegistered>(new(Id, appId, title, description, CatalogStatus.Empty, Version.Initial));
    }

    public void Activate()
    {
        if (Status is CatalogActive) return;
        if (Status is CatalogEmpty) throw new InvalidOperationException("Catalog is empty.");
        RaiseEvent<CatalogActivated>(new(Id, CatalogStatus.Active, Version.Next));
    }

    public void Deactivate()
    {
        if (Status is CatalogInactive) return;
        RaiseEvent<CatalogInactivated>(new(Id, CatalogStatus.Inactive, Version.Next));
    }

    public void ChangeCatalogTitle(Title title)
        => RaiseEvent(new CatalogTitleChanged(Id, title, Version.Next));

    public void ChangeDescription(Description description)
        => RaiseEvent(new CatalogDescriptionChanged(Id, description, Version.Next));

    public void Delete()
        => RaiseEvent(new CatalogDeleted(Id, CatalogStatus.Discarded, Version.Next));

    protected override void ApplyEvent(IDomainEvent @event) => When(@event as dynamic);

    private void When(CatalogRegistered @event)
    {
        Id = (CatalogId)@event.CatalogId;
        AppId = (AppId)@event.AppId;
        Title = (Title)@event.Title;
        Description = (Description)@event.Description;
        Version = (Version)@event.Version;
    }

    private void When(CatalogDescriptionChanged @event)
    {
        Description = (Description)@event.Description;
        Version = (Version)@event.Version;
    }

    private void When(CatalogTitleChanged @event)
    {
        Title = (Title)@event.Title;
        Version = (Version)@event.Version;
    }

    private void When(CatalogActivated @event)
    {
        Status = (CatalogStatus)@event.Status;
        Version = (Version)@event.Version;
    }

    private void When(CatalogInactivated @event)
    {
        Status = (CatalogStatus)@event.Status;
        Version = (Version)@event.Version;
    }

    private void When(CatalogDeleted @event)
    {
        Status = (CatalogStatus)@event.Status;
        IsDeleted = true;
        Version = (Version)@event.Version;
    }
}