using System;

namespace Messages.Catalogs.Commands
{
    public interface UpdateCatalogItem
    {
        Guid CatalogId { get; }
        Guid CatalogItemId { get; }
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
        string PictureUri { get; }
    }
}