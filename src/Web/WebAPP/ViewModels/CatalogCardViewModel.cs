using Contracts.Services.Catalog;
using WebAPP.HttpClients;

namespace WebAPP.ViewModels;

public class CatalogCardViewModel
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public static implicit operator CatalogCardViewModel(Projection.CatalogGridItem catalogGridItem)
        => new()
        {
            Description = catalogGridItem.Description,
            Id = catalogGridItem.Id,
            Title = catalogGridItem.Title,
            IsActive = catalogGridItem.IsActive,
            IsDeleted = catalogGridItem.IsDeleted
        };

    public static implicit operator CatalogCardViewModel(Requests.CreateCatalog request)
        => new()
        {
            Description = request.Description,
            Id = request.CatalogId,
            Title = request.Title
        };
}