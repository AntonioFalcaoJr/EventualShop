using System;

namespace Messages.Catalogs.Commands
{
    public interface RemoveCatalogItem
    {
        Guid CatalogId { get; }
        Guid CatalogItemId { get; }
    }
}