using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging;

public static class EventsV1
{
    public record CatalogCreationStarted;
    public record CatalogTitleChanged(string CatalogId, string NewTitle);
    public record CatalogTitleChangeFailed(string CatalogId, string Error);
    public record CatalogDescriptionChanged(string CatalogId, string NewDescription);
    public record CatalogDescriptionChangeFailed(string CatalogId, string Error);
    public record CatalogsListed(IPagedResult<Catalog> Catalogs);
    public record CatalogsListingFailed(string Error);
}