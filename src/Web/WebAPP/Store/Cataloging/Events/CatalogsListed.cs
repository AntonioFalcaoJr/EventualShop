using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogsListed
{
    public required IPagedResult<Catalog> Catalogs;
}