using Contracts.Services.Catalog;

namespace WebAPP.ViewModels;

public class CatalogCardViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public static implicit operator CatalogCardViewModel(Projection.Catalog catalog)
        => new()
        {
            Description = catalog.Description,
            Id = catalog.Id,
            Title = catalog.Title,
            IsActive = catalog.IsActive,
            IsDeleted = catalog.IsDeleted
        };

    public static implicit operator CatalogCardViewModel(Request.CreateCatalog request)
        => new()
        {
            Description = request.Description,
            Id = request.CatalogId,
            Title = request.Title
        };
}