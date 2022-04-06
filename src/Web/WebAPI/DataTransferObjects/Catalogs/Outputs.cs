using ECommerce.Abstractions.Messages.Queries.Paging;

namespace WebAPI.DataTransferObjects.Catalogs;

public static class Outputs
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted);

    public record CatalogItem(Guid Id, string Name, string Description, decimal Price, string PictureUri, bool IsDeleted);

    public record Catalogs(IEnumerable<Catalog> Items, PageInfo PageInfo) : IPagedResult<Catalog>;

    public record CatalogItems(IEnumerable<CatalogItem> Items, PageInfo PageInfo) : IPagedResult<CatalogItem>;
}