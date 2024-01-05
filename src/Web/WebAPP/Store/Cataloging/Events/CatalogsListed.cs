using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogsListed(IPagedResult<Catalog> Catalogs);