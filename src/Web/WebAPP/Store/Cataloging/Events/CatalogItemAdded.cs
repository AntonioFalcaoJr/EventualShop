using WebAPP.Store.Catalogs;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogItemAdded
{
    public required CatalogItem NewItem;
}