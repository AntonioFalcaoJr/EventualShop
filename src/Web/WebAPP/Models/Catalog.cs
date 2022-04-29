using Contracts.Services.Catalog;

namespace WebAPP.Models;

public record Catalog
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public bool IsEditingDescription { get; set; }
    public bool IsEditingTitle { get; set; }

    public void ToggleDescription()
        => IsEditingDescription = !IsEditingDescription;

    public void ToggleTitle()
        => IsEditingTitle = !IsEditingTitle;

    public static implicit operator Catalog(Projection.Catalog catalog)
        => new()
        {
            Description = catalog.Description,
            Id = catalog.Id,
            Title = catalog.Title,
            IsActive = catalog.IsActive,
            IsDeleted = catalog.IsDeleted
        };

    public static implicit operator Catalog(Request.CreateCatalog request)
        => new()
        {
            Description = request.Description,
            Id = request.CatalogId,
            Title = request.Title
        };
}