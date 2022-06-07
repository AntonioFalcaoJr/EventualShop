using Contracts.Services.Catalog;

namespace WebAPP.Models;

public class CatalogModel
{
    public CatalogModel()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public static implicit operator Request.CreateCatalog(CatalogModel model)
        => new(model.Id, model.Title, model.Description);

    public static implicit operator CatalogModel(Projection.Catalog catalog)
        => new()
        {
            Description = catalog.Description,
            Title = catalog.Title,
            IsActive = catalog.IsActive,
            IsDeleted = catalog.IsDeleted
        };

    public static implicit operator CatalogModel(Request.CreateCatalog request)
        => new()
        {
            Description = request.Description,
            Title = request.Title
        };
};