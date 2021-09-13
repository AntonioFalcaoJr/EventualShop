using System;

namespace Messages.Catalogs.Commands
{
    public interface AddCatalogItem
    {
        Guid CatalogId { get; }
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
        string PictureUri { get; }
    }
}